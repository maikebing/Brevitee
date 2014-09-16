using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee;
using Brevitee.Data;
using Brevitee.ServiceProxy;
using Brevitee.Logging;
using Brevitee.Analytics;
using Brevitee.Analytics.Data;
using Brevitee.Stickerize.Business;
using Brevitee.Stickerize.Business.Data;
using U = Brevitee.UserAccounts.Data;

using System.Reflection;

namespace Brevitee.Stickerize.Services
{
    [Proxy("stickerize")]
    [Serializable]
    public class StickerizeMe: IRequiresHttpContext
    {
        static StickerizeMe _current;
        static object _currentLock = new object();

        static StickerizeMe()
        {
            SQLiteRegistrar.Register<Sticker>();
            Db.TryEnsureSchema<Sticker>();
        }


        public IHttpContext HttpContext
        {
            get;
            set;
        }

		public Stickerizer CurrentStickerizer
		{
			get
			{
				return Stickerizer.Get(HttpContext);
			}
		}

        public static StickerizeMe Default
        {
            get
            {
                return _currentLock.DoubleCheckLock(ref _current, () => new StickerizeMe());
            }
        }

        public StickerizableListInfo AddList(string name)
        {
            Stickerizer er = Stickerizer.Get(HttpContext);
            StickerizableList list = StickerizableList.GetOrCreate(name, er.Id.Value);
			return new StickerizableListInfo(list);
        }
        
		public SubSectionInfo AddSubsection(int listId, string name)
		{
			StickerizableList list = StickerizableList.OneWhere(c => c.Id == listId);
			SubSection subSection = list.SubSectionsByStickerizableListId.AddNew();
			subSection.Created = DateTime.UtcNow;
			subSection.Name = name;
			subSection.Save();
			return new SubSectionInfo(subSection);
		}

		public object AddStickerizable(long subSectionId, string name)
		{
			SubSection subsection = SubSection.OneWhere(c => c.Id == subSectionId);
			return subsection.AddStickerizable(name, CurrentStickerizer.Id.Value, name).ToJsonSafe();			
		}

        public object AddStickerizee(string name, Gender gender) // called from the client
        {
            Stickerizer current = Stickerizer.Get(HttpContext);
            Stickerizee result = current.AddStickerizee(name, gender, name);
            return new StickerizeeInfo(result).ToJsonSafe();
        }

        public object RemoveStickerizee(long id) // called from the client
        {
            Stickerizer current = Stickerizer.Get(HttpContext);
            current.RemoveStickerizee(id);
            return new { Success = true }.ToJson();
        }

        public StickerizeeInfo[] GetStickerizees()
        {
            Stickerizer er = Stickerizer.Get(HttpContext);             
            StickerizeeInfo[] results = er.Stickerizees.Select(ee => new StickerizeeInfo(ee)).Reverse().ToArray();

            return results;
        }

        public StickerizeeInfo[] SearchStickerizees(string query)
        {
            StickerizeeCollection ees = Stickerizee.Where(c => c.Name.Contains(query));
            StickerizeeInfo[] results = ees.Select(ee => new StickerizeeInfo(ee)).ToArray();

            return results;
        }

		public StickerizableListInfo GetStickerizableList(long id)
		{
			Stickerizer er = Stickerizer.Get(HttpContext);
			StickerizableList list = StickerizableList.OneWhere(c=> 
			{
				return (c.CreatorId == er.Id && c.Id == id).Or(c.Public == true && c.Id == id);
			});

			return new StickerizableListInfo(list);
		}

        public StickerizableListInfo[] GetStickerizableLists()
        {
			StickerizableListCollection lists = StickerizableList.Where(c => c.Public == true || c.CreatorId == CurrentStickerizer.Id);
            StickerizableListInfo[] result = new StickerizableListInfo[lists.Count];
            lists.Each((l, i) =>
            {
                result[i] = new StickerizableListInfo(l);
            });

            return result;
        }

        public object[] GetSections(long listId)
        {
            StickerizableList list = StickerizableList.OneWhere(c => c.Id == listId);
            object[] result = new object[list.SubSectionsByStickerizableListId.Count];
            list.SubSectionsByStickerizableListId.Each((section, i) =>
            {
                result[i] = section.ToJsonSafe();
            });

            return result;
        }

        public object[] GetStickerizables(long sectionId)
        {
            SubSection section = SubSection.OneWhere(c => c.Id == sectionId);
            object[] results = new object[section.Stickerizables.Count];
            section.Stickerizables.Each((izable, i) =>
            {
                results[i] = izable.ToJsonSafe();
            });

            return results;
        }

        public CreateResult<object> Stickerize(DateTime at, long stickerizableId, long stickerizeeId)
        {
            try
            {
                Stickerization stickerization = Stickerizer.Get(HttpContext).Stickerize(at, stickerizableId, stickerizeeId);
                return new CreateResult<object>(stickerization.ToJsonSafe());
            }
            catch (Exception ex)
            {
                return new CreateResult<object>(ex);
            }
        }

        public UpdateResult<object> Unstickerize(DateTime at, long stickerizable, long stickerizeeId)
        {
            try
            {
                Stickerization stickerization = Stickerizer.Get(HttpContext).Stickerize(at, stickerizable, stickerizeeId);
                stickerization.Unstickerize();
                return new UpdateResult<object>(stickerization.ToJsonSafe());
            }catch(Exception ex)
            {
                return new UpdateResult<object>(ex);
            }
        }

        public RetrieveResult<object> GetStickerizations(DateTime forDate, long stickerizeeId)
        {
            try
            {
                StickerizationCollection stickerizations =  Stickerization.Where(c => c.StickerizeeId == stickerizeeId && c.ForDate == forDate.Date);//CreationId.In(creationIds));                
                return new RetrieveResult<object>(stickerizations.ToJsonSafe());
            }
            catch (Exception ex)
            {
                return new RetrieveResult<object>(ex);
            }
        }

        public FeedItem[] GetTestItems(int count)
        {
            FeedItem[] result = new FeedItem[count];
            count.Times(i =>
            {
                FeedItem item = new FeedItem(); 
                result[i] = new FeedItem();
            });
            
            return result;
        }
                
        public FeedItem[] GetFeedItems()
        {
            try
            {
                StickerizationCollection izations = Stickerization.Top(20, c => c.Id != null, Order.By<StickerizationColumns>(c => c.Id, SortOrder.Descending));
                FeedItem[] result = new FeedItem[izations.Count];
                izations.Each((s, i) =>
                {
                    result[i] = new FeedItem(s);
                });

                return result;
            }
            catch (Exception ex)
            {
                Log.AddEntry("{0}: {1}", ex, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }
    }
}

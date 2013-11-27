using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data;

namespace SampleData
{
    public partial class WishList
    {
        public static event WishListDelegate WishListCreated;

        protected static void OnWishListCreated(WishList w)
        {
            if (WishListCreated != null)
            {
                WishListCreated(w);
            }
        }

        public static WishList Create(User user, string name, string description = "")
        {
            WishList list = WishList.OneWhere(c => c.UserId == user.Id && c.Name == name);
            if (list == null)
            {
                list = new WishList();
                list.Name = name;
                list.Description = description;
                list.UserId = user.Id;
                list.Save();
                OnWishListCreated(list);
            }
            user.ResetChildren();
            return list;
        }

        public ItemCollection GetItems()
        {
            return GetItemsForWantIds(GetAllWantIds());
        }

        public ItemCollection GetItems(int firstCount)
        {
            long[] wantIds = GetAllWantIds();
            if (wantIds.Length > 0)
            {
                long[] chopped = new long[firstCount];
                for (int i = 0; i < firstCount; i++)
                {
                    if (wantIds.Length > i)
                    {
                        chopped[i] = wantIds[i];
                    }
                }

                return GetItemsForWantIds(chopped);
            }
            else
            {
                return new ItemCollection();
            }
        }

        private long[] GetAllWantIds()
        {
            long[] wantIds = this.WantWishListCollectionByWishListId.OrderByDescending(w => w.Id).Select(w => w.WantId.GetValueOrDefault()).ToArray();
            return wantIds;
        }

        private static ItemCollection GetItemsForWantIds(long[] chopped)
        {
            if (chopped.Length > 0)
            {
                QuerySet query = new QuerySet();
                query.Select<Want>("ItemId").Where<WantColumns>(c => c.Id.In(chopped));
                query.Execute(_.Db.For<Want>());
                WantCollection wants = query.Results[0].As<WantCollection>();
                long[] itemIds = wants.Select(c => c.ItemId.GetValueOrDefault()).ToArray();
                return Item.Where(c => c.Id.In(itemIds));
            }
            else
            {
                return new ItemCollection();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee;

namespace Brevitee.Stickerize.Business.Data
{
    public partial class StickerizableList
    {
        public static StickerizableList GetById(long id)
        {
            return StickerizableList.OneWhere(c => c.Id == id);
        }

        /// <summary>
        /// Creates the list with the specified name returning
        /// an existing one if one already exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static StickerizableList GetOrCreate(string name, long creatorId = 99999)
        {
            StickerizableList list = GetByName(name, creatorId);
			if (list == null)
			{
				list = Create(name, creatorId);
			}
			return list;
        }

        /// <summary>
        /// Retrieves the list with the specified name that was created by the specified
		/// creatorId returning an existing one if one already exists.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static StickerizableList GetByName(string name, long creatorId = 99999)
        {
            StickerizableList result = StickerizableList.OneWhere(c => c.Name == name && c.CreatorId == creatorId);    

            return result;
        }

		/// <summary>
		/// Create a StickerizableList with the specified name setting the 
		/// creator to the specified creatorId
		/// </summary>
		/// <param name="name"></param>
		/// <param name="creatorId"></param>
		/// <returns></returns>
		public static StickerizableList Create(string name, long creatorId = 99999)
		{
			StickerizableList result = new StickerizableList();
			result = new StickerizableList();
			result.Name = name;
			result.Public = false;
			result.Created = DateTime.UtcNow.Date;
			result.CreatorId = creatorId;
			result.Save();
			return result;
		}

		/// <summary>
		/// Add a subsection to the current StickerizableList
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
        public SubSection AddSubSection(string name)
        {
            SubSection result = SubSection.OneWhere(c => c.StickerizableListId == this.Id && c.Name == name);
            if(result == null)
            {
                result = this.SubSectionsByStickerizableListId.AddNew();
                result.Name = name;
                result.Created = DateTime.UtcNow.Date;// Date to remove the time portion
                this.Save();
            }
            return result;
        }


        /// <summary>
        /// Adds the specified Stickerizable to the current
        /// StickerizableList if it hasn't been added already
        /// </summary>
        /// <param name="stickerizable"></param>
        public Stickerizable AddStickerizable(Stickerizable stickerizable)
        {
            if (stickerizable.IsNew)
            {
                stickerizable.Save();
            }

            if (!Has(stickerizable))
            {
                this.Stickerizables.Add(stickerizable);
                this.Save();
            }

			return stickerizable;
        }

        public void RemoveStickerizable(long stickerizableId)
        {
            RemoveStickerizable(Stickerizable.OneWhere(c => c.Id == stickerizableId));
        }

        public void RemoveStickerizable(Stickerizable stickerizable)
        {
            if (stickerizable == null)
            {
                return;
            }

            this.Stickerizables.Remove(stickerizable);
        }

        public bool Has(Stickerizable item)
        {
            Expect.IsNotNull(item.Id);
            return Has(item.Id.Value);
        }

        public bool Has(long id)
        {
            return GetStickerizable(id) != null;
        }

        public Stickerizable GetStickerizable(long id)
        {
            return (from item in this.Stickerizables
                    where item.Id == id
                    select item).FirstOrDefault();
        }
    }
}

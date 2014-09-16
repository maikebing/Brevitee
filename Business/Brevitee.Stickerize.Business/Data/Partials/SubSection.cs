using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business.Data
{
    public partial class SubSection
    {
		static SubSection()
		{
			Dao.PostConstructActions[typeof(SubSection)] = (ss) =>
			{
				SubSection s = (SubSection)ss;
				s.Created = DateTime.UtcNow;
			};
		}
        public Stickerizable AddStickerizable(string name, long creatorId = 99999, string _for = null)
        {
            Stickerizable result = Stickerizables.Where(s => s.Name == name && s.CreatorId == creatorId).FirstOrDefault();
            if (result == null)
            {
                if (string.IsNullOrEmpty(_for))
                {
                    _for = name;
                }
				
                result = this.Stickerizables.AddNew();
                result.For = _for;
                result.Name = name;
                result.Created = DateTime.UtcNow.Date;
				result.CreatorId = creatorId;
                this.Save();
            }

            return result;
        }
    }
}

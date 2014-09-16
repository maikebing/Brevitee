using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Data;
using Brevitee.Stickerize.Business.Data;

namespace Brevitee.Stickerize.Business
{
    /// <summary>
    /// Acts as a DTO for StickerizableList
    /// </summary>
    public class StickerizableListInfo
    {
        public StickerizableListInfo(StickerizableList list)
        {
			this.Id = list == null ? -1: list.Id ?? -1;
            this.Name = list == null ? "N/A" : list.Name;
			this.Public = list.Public ?? true;
			this.CreatorId = list.CreatorId ?? StickerizeIntializer.SystemId;

			if (list != null && list.SubSectionsByStickerizableListId.Count > 0)
			{
				SubSectionInfo[] subSections = new SubSectionInfo[list.SubSectionsByStickerizableListId.Count];
				list.SubSectionsByStickerizableListId.Each((s, i) =>
				{
					subSections[i] = new SubSectionInfo(s);
				});
				this.SubSections = subSections;
			}
			else
			{
				this.SubSections = new SubSectionInfo[] { };
			}
        }

		public long Id { get; set; }

        public string Name { get; set; }

		public bool Public { get; set; }

		public long CreatorId { get; set; }
        public SubSectionInfo[] SubSections { get; set; }
    }
}

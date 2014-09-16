using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Data;
using Brevitee.Stickerize.Business.Data;

namespace Brevitee.Stickerize.Business
{
    public class SubSectionInfo
    {
        public SubSectionInfo(SubSection subSection)
        {
			this.Id = subSection.Id.Value;
            this.Name = subSection.Name;

            if (subSection.Name.Contains(":"))
            {
                string[] split = subSection.Name.DelimitSplit(":", true);
                this.Name = split[0];
                this.SecondaryName = split[1];
            }
            this.Stickerizables = subSection.Stickerizables.ToJsonSafe();
        }

		public long Id { get; set; }

        public string Name { get; set; }
        public string SecondaryName { get; set; }
        public object[] Stickerizables { get; set; }
    }
}

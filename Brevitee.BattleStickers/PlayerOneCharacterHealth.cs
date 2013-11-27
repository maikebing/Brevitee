using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.BattleStickers
{
    public partial class PlayerOneCharacterHealth: IHealth
    {
        #region IHealth Members

        public int Health
        {
            get
            {
                return this.Value.Value;
            }
            set
            {
                this.Value = value;
                this.Save();
            }
        }

        #endregion
    }
}

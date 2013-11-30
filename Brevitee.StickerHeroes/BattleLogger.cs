using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Logging;

namespace Brevitee.StickerHeroes
{
    public class BattleLogger//: Logger
    {
        public event Action<Battle, string> EntryAdded;

        List<string> _lines;
        public  BattleLogger(Battle battle)
        {
            this.Battle = battle;
            this._lines = new List<string>();
        }

        public Battle Battle
        {
            get;
            set;
        }

        protected virtual void OnEntryAdded(string line)
        {
            if (EntryAdded != null)
            {
                EntryAdded(Battle, line);
            }
        }

        public void AddEntry(string line)
        {
            _lines.Add(line);
            OnEntryAdded(line);
        }

        //public void AddEntry(Battle battle, string messageSignature, params object[] formatVariables)
        //{

        //}
    }
}

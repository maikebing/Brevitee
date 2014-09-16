using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business
{
    public class StickerizableBuilder
    {
        public StickerizableBuilder()
        {
            throw new NotImplementedException("refer to http://oxforddictionaries.com/us/words/verb-tenses-adding-ed-and-ing");
        }

        public string Verb { get; set; }

        bool _pastTense;
        public bool PastTense
        {
            get
            {
                return _pastTense;
            }
            set
            {
                _pastTense = value;
                _presentParticiple = !value;
            }
        }

        bool _presentParticiple;
        public bool PresentParticiple
        {
            get
            {
                return _presentParticiple;
            }
            set
            {
                _presentParticiple = value;
                _pastTense = !value;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Analytics.Crawlers
{
    public class ActionChangedEventArgs: EventArgs
    {
        public ActionChangedEventArgs(CrawlerState.Action oldAction, CrawlerState.Action newAction)
        {
            this.OldAction = oldAction;
            this.NewAction = newAction;
        }

        public CrawlerState.Action OldAction { get; set; }
        public CrawlerState.Action NewAction { get; set; }
    }
}

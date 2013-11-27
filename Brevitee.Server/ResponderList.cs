using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Server
{
    public class ResponderList: ResponderBase
    {
        List<IResponder> _responders;
        public ResponderList(Fs fs, IEnumerable<IResponder> responders)
            : base(fs)
        {
            this._responders = new List<IResponder>(responders);
        }

        public void AddResponders(params IResponder[] responder)
        {
            _responders.AddRange(responder);
        }

        public override bool MayHandle(IContext context)
        {
            return true;
        }

        protected override void LogRequest(string path)
        {
            // overridden with the express intent of turning it off for the ResponderList
        }

        #region IResponder Members

        public override bool TryRespond(IContext context)
        {
            bool handled = false;
            foreach (IResponder r in _responders)
            {
                if (r.Respond(context))
                {
                    handled = true;
                    break;
                }
            }

            return handled;
        }

        #endregion
    }
}

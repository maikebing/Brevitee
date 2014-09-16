using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.ServiceProxy;

namespace Brevitee.Server
{
    public class ResponderList: ResponderBase
    {
        List<IResponder> _responders;
        public ResponderList(BreviteeConf conf, IEnumerable<IResponder> responders)
            : base(conf)
        {
            this._responders = new List<IResponder>(responders);
        }

        public void AddResponders(params IResponder[] responder)
        {
            _responders.AddRange(responder);
        }

        public override bool MayRespond(IHttpContext context)
        {
            return true;
        }
        
        #region IResponder Members

        public override bool TryRespond(IHttpContext context)
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

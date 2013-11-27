using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleData
{
    public partial class AddressRequestComment : ICommentXref
    {
        #region ICommentXref Members

        public long ParentId
        {
            get
            {
                return this.AddressRequestId.GetValueOrDefault();
            }
            set
            {
                this.AddressRequestId = value;
            }
        }

        public string CommentType
        {
            get { return "address request"; }
        }

        #endregion
    }
}

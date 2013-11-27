using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleData
{
    public partial class ActivityComment: ICommentXref
    {
        #region ICommentXref Members

        public long ParentId
        {
            get
            {
                return this.ActivityId.GetValueOrDefault();
            }
            set
            {
                this.ActivityId = value;
            }
        }        

        public string CommentType
        {
            get { return "activity"; }
        }

        #endregion
    }
}

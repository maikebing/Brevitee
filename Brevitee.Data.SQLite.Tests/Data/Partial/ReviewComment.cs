using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleData
{
    public partial class ReviewComment: ICommentXref
    {
        #region ICommentXref Members

        public long ParentId
        {
            get
            {
                return this.ReviewId.GetValueOrDefault();
            }
            set
            {
                this.ReviewId = value;
            }
        }

        public string CommentType
        {
            get { return "review"; }
        }

        #endregion
    }
}

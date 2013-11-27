using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleData
{
    public interface ICommentXref
    {
        long ParentId { get; set; }
        string CommentType { get; }
        long? CommentId { get; set; }
        void Save();
    }
}

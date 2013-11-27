using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.Data;
using Brevitee.Data;

namespace SampleData
{
    public partial class Activity
    {
        protected static WhereDelegate<ActivityColumns> AdminFilter
        {
            get
            {
                return (c) => c.DateTime < DateTime.UtcNow;
            }
        }

        protected static WhereDelegate<ActivityColumns> UserFilter
        {
            get
            {
                return (c) => c.DateTime < DateTime.UtcNow && c.UserId != User.System().Id;
            }
        }

        /// <summary>
        /// Gets the latest 3 comments for the current activity if any.
        /// </summary>
        public CommentCollection Comments(int top = 3)
        {
            ActivityCommentCollection xrefs = this.ActivityCommentCollectionByActivityId;

            long[] commentIds = xrefs.Select(c => c.CommentId.GetValueOrDefault()).ToArray();

            CommentCollection comments = new CommentCollection();
            if (commentIds.Length > 0)
            {
                comments =Comment.Top(top, c => c.Id.In(commentIds), new OrderBy<CommentColumns>(c => c.Created, SortOrder.Descending));
            }

            return comments;
        }

        /// <summary>
        /// Gets the specified number of activites excluding the system user
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static ActivityCollection UserActivities(int count)
        {
            return Activity.Top(count, UserFilter, new OrderBy<ActivityColumns>(c => c.DateTime, SortOrder.Descending));            
        }

        /// <summary>
        /// Gets the specified number of activities including activity from the system user
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static ActivityCollection AdminActivities(int count)
        {
            return Activity.Top(count, AdminFilter, new OrderBy<ActivityColumns>(c => c.DateTime, SortOrder.Descending));
        }

        /// <summary>
        /// Gets the specified number of activities from the specified date
        /// </summary>
        /// <returns></returns>
        public static ActivityCollection CountFrom(int count, DateTime dateTime)
        {
            return Activity.Top(count, c => c.DateTime < dateTime && c.UserId != User.System().Id);
        }

        public static ActivityCollection CountFromWithSystem(int count, DateTime dateTime)
        {
            return Activity.Top(count, c => c.DateTime < dateTime);
        }
    }
}

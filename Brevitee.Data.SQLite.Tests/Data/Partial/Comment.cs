using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleData
{
    public partial class Comment
    {
        public Comment(string comment, long userId)
            : this()
        {
            this.Text = comment;
            this.Created = DateTime.UtcNow;
            this.Modified = DateTime.UtcNow;
            this.UserId = userId;
        }

        /// <summary>
        /// Calls Save and returns the current comment
        /// </summary>
        /// <returns></returns>
        public Comment Write()
        {
            this.Save();
            return this;
        }
        
        public static Comment[] Create(long userId, params string[] comments)
        {
            Comment[] results = new Comment[comments.Length];
            for (int i = 0; i < comments.Length; i++)
            {
                results[i] = new Comment(comments[i], userId)
                                    .Write();
            }

            return results;
        }

        public static Comment On(AddressRequest addressRequest, string comment, User commenter)
        {
            return On<AddressRequestComment>(addressRequest.Id.GetValueOrDefault(), comment, commenter.Id.GetValueOrDefault());
        }

        public static Comment OnAddressRequest(long addressRequestId, string comment, User user)
        {
            return On<AddressRequestComment>(addressRequestId, comment, user.Id.GetValueOrDefault());
        }
        
        public static Comment On(Review review, string comment, User commenter)
        {
            return On<ReviewComment>(review.Id.GetValueOrDefault(), comment, commenter.Id.GetValueOrDefault());
        }

        public static Comment OnReview(long reviewId, string comment, User commenter)
        {
            return On<ReviewComment>(reviewId, comment, commenter.Id.GetValueOrDefault());
        }
        
        public static Comment On(Activity activity, string comment, User commenter)
        {
            return On<ActivityComment>(activity.Id.GetValueOrDefault(), comment, commenter.Id.GetValueOrDefault());
        }

        public static Comment OnActivity(long activityId, string comment, User commenter)
        {
            ActivityComment ignore;
            return OnActivity(activityId, comment, commenter, out ignore);
        }

        public static Comment OnActivity(long activityId, string comment, User commenter, out ActivityComment xref)
        {            
            return On<ActivityComment>(activityId, comment, commenter.Id.GetValueOrDefault(), out xref);
        }

        protected static Comment On<T>(long parentId, string comment, long userId) where T : ICommentXref, new()
        {
            T ignore;
            return On<T>(parentId, comment, userId, out ignore);
        }

        protected static Comment On<T>(long parentId, string comment, long userId, out T xref) where T : ICommentXref, new()
        {
            Comment c = Create(userId, comment)[0];
            T toBeSaved = new T();
            toBeSaved.ParentId = parentId;
            toBeSaved.CommentId = c.Id;
            toBeSaved.Save();
            xref = toBeSaved;
            return c;
        }
    }
}

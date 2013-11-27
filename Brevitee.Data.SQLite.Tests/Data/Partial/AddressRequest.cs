using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Data;
using Brevitee;
using Brevitee.Data;
using System.Linq;
using System.Linq.Expressions;
using Brevitee.FileExt;
using Brevitee.FileExt.Js;

namespace SampleData
{
    public partial class AddressRequest
    {
        public Address Address
        {
            get
            {
                UserAddress xref = UserAddress.OneWhere(c => c.UserId == this.RequesteeId);
                SampleData.Address result = null;
                if (xref != null)
                {
                    result = SampleData.Address.OneWhere(c => c.Id == xref.AddressId);
                }
                return result;
            }
        }

        public static AddressRequest Create(User requester, User requestee)
        {
            return Create(requester.Id.GetValueOrDefault(), requestee.Id.GetValueOrDefault());
        }

        public static AddressRequest Create(long requesterId, long requesteeId)
        {
            if (requesterId <= 0 || requesteeId <= 0)
            {
                throw Args.Exception<InvalidOperationException>("One or both of the specified user ids is invalid: requester=({0}); requestee=({1})", requesterId, requesteeId);
            }
            AddressRequest result = new AddressRequest();
            result.RequesterId = requesterId;
            result.RequesteeId = requesteeId;
            result.Approved = false;
            result.Save();
            return result;
        }

        public AddressRequest Comment(string comment, bool save = true)
        {
            return Comment(this.RequesterId.GetValueOrDefault(), comment, save);
        }

        public AddressRequest Comment(long userId, string comment, bool save = true)
        {
            AddComments(SampleData.Comment.Create(userId, comment));
            return this;
        }

        /// <summary>
        /// Adds the specified comments and returns all comments for the AddressRequest
        /// </summary>
        /// <param name="comments"></param>
        /// <returns></returns>
        public CommentCollection Comments(params Comment[] comments)
        {
            if (comments != null || comments.Length > 0)
            {
                AddComments(comments);
            }

            QuerySet query = new QuerySet();
            query.Select<AddressRequestComment>(new AddressRequestCommentColumns().CommentId.ToString())
                .Where<AddressRequestCommentColumns>(c => c.AddressRequestId == this.Id);

            query.Execute(_.Db.For<Comment>());

            long[] commentIds = query.Results[0].As<AddressRequestCommentCollection>().Select(ar => ar.CommentId.GetValueOrDefault()).ToArray();
            CommentCollection results = new CommentCollection();
            if (commentIds.Length > 0)
            {
                results = SampleData.Comment.Where(c => c.Id.In(commentIds));
            }

            return results;
        }

        private void AddComments(Comment[] comments)
        {
            foreach (Comment comment in comments)
            {
                if (comment.IsNew)
                {
                    comment.Save();
                }

                AddressRequestComment xref = this.AddressRequestCommentCollectionByAddressRequestId.AddNew();
                xref.CommentId = comment.Id;
            }
            this.Save();
        }
    }
}

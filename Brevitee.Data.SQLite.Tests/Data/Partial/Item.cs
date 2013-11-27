using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.Data;
using Brevitee.Hatagi;

namespace SampleData
{
    public partial class Item : IItem
    {
        ReviewData[] _reviewData;
        public ReviewData[] Reviews
        {
            get
            {
                if (_reviewData == null)
                {
                    _reviewData = GetReviews();
                }

                return _reviewData;
            }
        }

        private ReviewData[] GetReviews()
        {
            ItemReviewCollection xrefs = ItemReview.Where(c => c.ItemId == this.Id);
            long[] reviewIds = xrefs.Select(i=>i.ReviewId.GetValueOrDefault()).ToArray();
            long[] userIds = xrefs.Select(i=>i.UserId.GetValueOrDefault()).ToArray();
            ReviewData[] results = new ReviewData[xrefs.Count];
            if (xrefs.Count > 0)
            {
                Dictionary<long, Review> keyedReviews = Review.Where(c => c.Id.In(reviewIds)).ToDictionary(r => r.Id.GetValueOrDefault());
                Dictionary<long, User> keyedUsers = User.Where(c => c.Id.In(userIds)).ToDictionary(u => u.Id.GetValueOrDefault());
                for (int i = 0; i < xrefs.Count; i++)
                {
                    ItemReview xref = xrefs[i];
                    Review review = keyedReviews[xref.ReviewId.GetValueOrDefault()];
                    User user = keyedUsers[xref.UserId.GetValueOrDefault()];
                    results[i] = new ReviewData(review, user);
                }
            }

            return results;
        }

        public bool WasReviewedBy(User user)
        {
            Review review;
            return WasReviewedBy(user, out review);
        }

        public bool WasReviewedBy(User user, out Review review)
        {
            ItemReview xref = ItemReview.OneWhere(c => c.ItemId == this.Id && c.UserId == user.Id);
            review = null;
            bool result = false;
            if (xref != null)
            {
                review = xref.ReviewOfReviewId;
                result = true;
            }

            return result;            
        }

        public static Item WhereAsinEquals(string asin)
        {
            QuerySet query = new QuerySet();
            query.Select<ItemProperty>("ItemId").Where<ItemPropertyColumns>(c => c.Value == asin);
            query.Execute(_.Db.For<ItemProperty>());

            ItemPropertyCollection props = query.Results[0].As<ItemPropertyCollection>();
            Item item = null;
            if (props.Count == 1)
            {
                long id = props.Select<ItemProperty, long>(p => p.ItemId.GetValueOrDefault()).ToArray()[0];
                item = Item.OneWhere(c => c.Id == id);
            }
            else if (props.Count > 1)
            {
                throw new InvalidOperationException(string.Format("Multiple entries found for ASIN {0}", asin));
            }
            return item;
        }

        public string SubTitle
        {
            get { return GetItemProperty(SubTitleProperty); }
        }

        string _subTitleProperty = "Platform";
        public string SubTitleProperty
        {
            get
            {
                return _subTitleProperty;
            }
            set
            {
                _subTitleProperty = value;
            }
        }

        public string GetItemProperty(string propertyName)
        {
            string val = (from p in this.ItemPropertyCollectionByItemId
                          where p.Name.Equals(propertyName)
                          select p.Value).FirstOrDefault();

            return string.IsNullOrEmpty(val) ? string.Empty : val;
        }

        public void SetItemProperty(string propertyName, string value)
        {
            ItemProperty existing = (from p in this.ItemPropertyCollectionByItemId
                                     where p.Name.Equals(propertyName)
                                     select p).FirstOrDefault();
            if (existing == null)
            {
                existing = this.ItemPropertyCollectionByItemId.AddNew();
            }
            existing.Name = propertyName;
            existing.Value = value ?? string.Empty;
            existing.Save();
        }

        public string Description
        {
            get
            {
                string value = GetItemProperty("Description");
                return string.IsNullOrEmpty(value) ? "No description available" : value;
            }
        }

        public string ASIN
        {
            get
            {
                return GetItemProperty("ASIN");
            }
        }

        public string DetailPage
        {
            get
            {
                return GetItemProperty("DetailPage");
            }
        }

        public string LargeImageURL
        {
            get
            {
                return GetItemProperty("LargeImageURL");
            }
        }

        public string SmallImageURL
        {
            get
            {
                return GetItemProperty("SmallImageURL");
            }
        }
        public string MediumImageURL
        {
            get
            {
                return GetItemProperty("MediumImageURL");
            }
        }

        public string Genre
        {
            get
            {
                return GetItemProperty("Genre");
            }
        }

        public string ReleaseDate
        {
            get
            {
                return GetItemProperty("ReleaseDate");
            }            
        }

        public string ESRBRating
        {
            get
            {
                return GetItemProperty("ESRBRating");
            }
        }

    }
}

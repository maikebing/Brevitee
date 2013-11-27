using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SampleData;
using Brevitee;

namespace SampleData
{
    public class ReviewData
    {
        public ReviewData(Review review)
        {
            this.Review = review;
        }

        public ReviewData(Review review, User user)
            : this(review)
        {
            this.User(user);
        }

        public Review Review
        {
            get;
            private set;
        }

        public int Rating
        {
            get
            {
                return Review.Rating.GetValueOrDefault();
            }
        }

        User _user;
        public User User(User user = null)
        {
            if (user != null)
            {

                _user = user;
            }

            return _user;
        }

        public string Text
        {
            get
            {
                return Review.Text;
            }
        }

        public string Title
        {
            get
            {
                return Review.Title;
            }
        }
    }
}

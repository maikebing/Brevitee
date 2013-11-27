using System;

namespace Brevitee.Schema.Org
{
	///<summary>The rating of the video.</summary>
	public class Rating: Intangible
	{
		///<summary>The highest value allowed in this rating system. If bestRating is omitted, 5 is assumed.</summary>
		public ThisOrThat<Number, Text> BestRating {get; set;}
		///<summary>The rating for the content.</summary>
		public Text RatingValue {get; set;}
		///<summary>The lowest value allowed in this rating system. If worstRating is omitted, 1 is assumed.</summary>
		public ThisOrThat<Number, Text> WorstRating {get; set;}
	}
}

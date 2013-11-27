using System;

namespace Brevitee.Schema.Org
{
	///<summary>When a single product that has different offers (for example, the same pair of shoes is offered by different merchants), then AggregateOffer can be used.</summary>
	public class AggregateOffer: Offer
	{
		///<summary>The highest price of all offers available.</summary>
		public ThisOrThat<Number, Text> HighPrice {get; set;}
		///<summary>The lowest price of all offers available.</summary>
		public ThisOrThat<Number, Text> LowPrice {get; set;}
		///<summary>The number of offers for the product.</summary>
		public Integer OfferCount {get; set;}
	}
}

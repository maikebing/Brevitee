using System;

namespace Brevitee.Schema.Org
{
	///<summary>A contact point—for example, a Customer Complaints department.</summary>
	public class ContactPoint: StructuredValue
	{
		///<summary>A person or organization can have different contact points, for different purposes. For example, a sales contact point, a PR contact point and so on. This property is used to specify the kind of contact point.</summary>
		public Text ContactType {get; set;}
		///<summary>Email address.</summary>
		public Text Email {get; set;}
		///<summary>The fax number.</summary>
		public Text FaxNumber {get; set;}
		///<summary>The telephone number.</summary>
		public Text Telephone {get; set;}
	}
}

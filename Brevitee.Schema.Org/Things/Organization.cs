using System;

namespace Brevitee.Schema.Org
{
	///<summary>An organization such as a school, NGO, corporation, club, etc.</summary>
	public class Organization: Thing
	{
		///<summary>Physical address of the item.</summary>
		public PostalAddress Address {get; set;}
		///<summary>The overall rating, based on a collection of reviews or ratings, of the item.</summary>
		public AggregateRating AggregateRating {get; set;}
		///<summary>The brand(s) associated with a product or service, or the brand(s) maintained by an organization or business person.</summary>
		public ThisOrThat<Brand, Organization> Brand {get; set;}
		///<summary>A contact point for a person or organization.</summary>
		public ContactPoint ContactPoint {get; set;}
		///<summary>A contact point for a person or organization (legacy spelling; see singular form, contactPoint).</summary>
		public ContactPoint ContactPoints {get; set;}
		///<summary>The Dun & Bradstreet DUNS number for identifying an organization or business person.</summary>
		public Text Duns {get; set;}
		///<summary>Email address.</summary>
		public Text Email {get; set;}
		///<summary>Someone working for this organization.</summary>
		public Person Employee {get; set;}
		///<summary>People working for this organization. (legacy spelling; see singular form, employee)</summary>
		public Person Employees {get; set;}
		///<summary>Upcoming or past event associated with this place or organization.</summary>
		public Event Event {get; set;}
		///<summary>Upcoming or past events associated with this place or organization (legacy spelling; see singular form, event).</summary>
		public Event Events {get; set;}
		///<summary>The fax number.</summary>
		public Text FaxNumber {get; set;}
		///<summary>A person who founded this organization.</summary>
		public Person Founder {get; set;}
		///<summary>A person who founded this organization (legacy spelling; see singular form, founder).</summary>
		public Person Founders {get; set;}
		///<summary>The date that this organization was founded.</summary>
		public Date FoundingDate {get; set;}
		///<summary>The Global Location Number (GLN, sometimes also referred to as International Location Number or ILN) of the respective organization, person, or place. The GLN is a 13-digit number used to identify parties and physical locations.</summary>
		public Text GlobalLocationNumber {get; set;}
		///<summary>Points-of-Sales operated by the organization or person.</summary>
		public Place HasPOS {get; set;}
		///<summary>A count of a specific user interactions with this item—for example, 20 UserLikes, 5 UserComments, or 300 UserDownloads. The user interaction type should be one of the sub types of UserInteraction.</summary>
		public Text InteractionCount {get; set;}
		///<summary>The International Standard of Industrial Classification of All Economic Activities (ISIC), Revision 4 code for a particular organization, business person, or place.</summary>
		public Text IsicV4 {get; set;}
		///<summary>The official name of the organization, e.g. the registered company name.</summary>
		public Text LegalName {get; set;}
		///<summary>The location of the event or organization.</summary>
		public ThisOrThat<Place, PostalAddress> Location {get; set;}
		///<summary>URL of an image for the logo of the item.</summary>
		public ThisOrThat<ImageObject, URL> Logo {get; set;}
		///<summary>A pointer to products or services offered by the organization or person.</summary>
		public Offer MakesOffer {get; set;}
		///<summary>A member of this organization.</summary>
		public ThisOrThat<Organization, Person> Member {get; set;}
		///<summary>A member of this organization (legacy spelling; see singular form, member).</summary>
		public ThisOrThat<Organization, Person> Members {get; set;}
		///<summary>The North American Industry Classification System (NAICS) code for a particular organization or business person.</summary>
		public Text Naics {get; set;}
		///<summary>Products owned by the organization or person.</summary>
		public ThisOrThat<OwnershipInfo, Product> Owns {get; set;}
		///<summary>A review of the item.</summary>
		public Review Review {get; set;}
		///<summary>Review of the item (legacy spelling; see singular form, review).</summary>
		public Review Reviews {get; set;}
		///<summary>A pointer to products or services sought by the organization or person (demand).</summary>
		public Demand Seeks {get; set;}
		///<summary>The Tax / Fiscal ID of the organization or person, e.g. the TIN in the US or the CIF/NIF in Spain.</summary>
		public Text TaxID {get; set;}
		///<summary>The telephone number.</summary>
		public Text Telephone {get; set;}
		///<summary>The Value-added Tax ID of the organisation or person.</summary>
		public Text VatID {get; set;}
	}
}

using System;

namespace Brevitee.Schema.Org
{
	///<summary>The most generic kind of creative work, including books, movies, photographs, software programs, etc.</summary>
	public class CreativeWork: Thing
	{
		///<summary>The subject matter of the content.</summary>
		public Thing About {get; set;}
		///<summary>Specifies the Person that is legally accountable for the CreativeWork.</summary>
		public Person AccountablePerson {get; set;}
		///<summary>The overall rating, based on a collection of reviews or ratings, of the item.</summary>
		public AggregateRating AggregateRating {get; set;}
		///<summary>A secondary title of the CreativeWork.</summary>
		public Text AlternativeHeadline {get; set;}
		///<summary>The media objects that encode this creative work. This property is a synonym for encodings.</summary>
		public MediaObject AssociatedMedia {get; set;}
		///<summary>The intended audience of the work, i.e. the group for whom the work was created.</summary>
		public Audience Audience {get; set;}
		///<summary>An embedded audio object.</summary>
		public AudioObject Audio {get; set;}
		///<summary>The author of this content. Please note that author is special in that HTML 5 provides a special mechanism for indicating authorship via the rel tag. That is equivalent to this and may be used interchangeably.</summary>
		public ThisOrThat<Organization, Person> Author {get; set;}
		///<summary>An award won by this person or for this creative work.</summary>
		public Text Award {get; set;}
		///<summary>Awards won by this person or for this creative work. (legacy spelling; see singular form, award)</summary>
		public Text Awards {get; set;}
		///<summary>Comments, typically from users, on this CreativeWork.</summary>
		public UserComments Comment {get; set;}
		///<summary>The location of the content.</summary>
		public Place ContentLocation {get; set;}
		///<summary>Official rating of a piece of content—for example,'MPAA PG-13'.</summary>
		public Text ContentRating {get; set;}
		///<summary>A secondary contributor to the CreativeWork.</summary>
		public ThisOrThat<Organization, Person> Contributor {get; set;}
		///<summary>The party holding the legal copyright to the CreativeWork.</summary>
		public ThisOrThat<Organization, Person> CopyrightHolder {get; set;}
		///<summary>The year during which the claimed copyright for the CreativeWork was first asserted.</summary>
		public Number CopyrightYear {get; set;}
		///<summary>The creator/author of this CreativeWork or UserComments. This is the same as the Author property for CreativeWork.</summary>
		public ThisOrThat<Organization, Person> Creator {get; set;}
		///<summary>The date on which the CreativeWork was created.</summary>
		public Date DateCreated {get; set;}
		///<summary>The date on which the CreativeWork was most recently modified.</summary>
		public Date DateModified {get; set;}
		///<summary>Date of first broadcast/publication.</summary>
		public Date DatePublished {get; set;}
		///<summary>A link to the page containing the comments of the CreativeWork.</summary>
		public URL DiscussionUrl {get; set;}
		///<summary>Specifies the Person who edited the CreativeWork.</summary>
		public Person Editor {get; set;}
		///<summary>A media object that encode this CreativeWork.</summary>
		public MediaObject Encoding {get; set;}
		///<summary>The media objects that encode this creative work (legacy spelling; see singular form, encoding).</summary>
		public MediaObject Encodings {get; set;}
		///<summary>Genre of the creative work</summary>
		public Text Genre {get; set;}
		///<summary>Headline of the article</summary>
		public Text Headline {get; set;}
		///<summary>The language of the content. please use one of the language codes from the IETF BCP 47 standard.</summary>
		public Text InLanguage {get; set;}
		///<summary>A count of a specific user interactions with this item—for example, 20 UserLikes, 5 UserComments, or 300 UserDownloads. The user interaction type should be one of the sub types of UserInteraction.</summary>
		public Text InteractionCount {get; set;}
		///<summary>Indicates whether this content is family friendly.</summary>
		public Boolean IsFamilyFriendly {get; set;}
		///<summary>The keywords/tags used to describe this content.</summary>
		public Text Keywords {get; set;}
		///<summary>Indicates that the CreativeWork contains a reference to, but is not necessarily about a concept.</summary>
		public Thing Mentions {get; set;}
		///<summary>An offer to sell this item—for example, an offer to sell a product, the DVD of a movie, or tickets to an event.</summary>
		public Offer Offers {get; set;}
		///<summary>Specifies the Person or Organization that distributed the CreativeWork.</summary>
		public ThisOrThat<Organization, Person> Provider {get; set;}
		///<summary>The publisher of the creative work.</summary>
		public Organization Publisher {get; set;}
		///<summary>Link to page describing the editorial principles of the organization primarily responsible for the creation of the CreativeWork.</summary>
		public URL PublishingPrinciples {get; set;}
		///<summary>A review of the item.</summary>
		public Review Review {get; set;}
		///<summary>Review of the item (legacy spelling; see singular form, review).</summary>
		public Review Reviews {get; set;}
		///<summary>The Organization on whose behalf the creator was working.</summary>
		public Organization SourceOrganization {get; set;}
		///<summary>The textual content of this CreativeWork.</summary>
		public Text Text {get; set;}
		///<summary>A thumbnail image relevant to the Thing.</summary>
		public URL ThumbnailUrl {get; set;}
		///<summary>The version of the CreativeWork embodied by a specified resource.</summary>
		public Number Version {get; set;}
		///<summary>An embedded video object.</summary>
		public VideoObject Video {get; set;}
	}
}

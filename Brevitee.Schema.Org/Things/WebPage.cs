using System;

namespace Brevitee.Schema.Org
{
	///<summary>A web page. Every web page is implicitly assumed to be declared to be of type WebPage, so the various properties about that webpage, such as</summary>
	public class WebPage: CreativeWork
	{
		///<summary>A set of links that can help a user understand and navigate a website hierarchy.</summary>
		public Text Breadcrumb {get; set;}
		///<summary>Indicates the collection or gallery to which the item belongs.</summary>
		public CollectionPage IsPartOf {get; set;}
		///<summary>Date on which the content on this web page was last reviewed for accuracy and/or completeness.</summary>
		public Date LastReviewed {get; set;}
		///<summary>Indicates if this web page element is the main subject of the page.</summary>
		public WebPageElement MainContentOfPage {get; set;}
		///<summary>Indicates the main image on the page</summary>
		public ImageObject PrimaryImageOfPage {get; set;}
		///<summary>A link related to this web page, for example to other related web pages.</summary>
		public URL RelatedLink {get; set;}
		///<summary>People or organizations that have reviewed the content on this web page for accuracy and/or completeness.</summary>
		public ThisOrThat<Organization, Person> ReviewedBy {get; set;}
		///<summary>One of the more significant URLs on the page. Typically, these are the non-navigation links that are clicked on the most.</summary>
		public URL SignificantLink {get; set;}
		///<summary>The most significant URLs on the page. Typically, these are the non-navigation links that are clicked on the most (legacy spelling; see singular form, significantLink).</summary>
		public URL SignificantLinks {get; set;}
		///<summary>One of the domain specialities to which this web page's content applies.</summary>
		public Specialty Specialty {get; set;}
	}
}

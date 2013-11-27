using System;

namespace Brevitee.Schema.Org
{
	///<summary>A scholarly article in the medical domain.</summary>
	public class MedicalScholarlyArticle: ScholarlyArticle
	{
		///<summary>A citation or reference to another creative work, such as another publication, web page, scholarly article, etc. NOTE: Candidate for promotion to ScholarlyArticle.</summary>
		public ThisOrThat<CreativeWork, Text> Citation {get; set;}
		///<summary>The type of the medical article, taken from the US NLM MeSH publication type catalog.</summary>
		public Text PublicationType {get; set;}
	}
}

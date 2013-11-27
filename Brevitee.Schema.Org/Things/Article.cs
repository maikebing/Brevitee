using System;

namespace Brevitee.Schema.Org
{
	///<summary>An article, such as a news article or piece of investigative report. Newspapers and magazines have articles of many different types and this is intended to cover them all.</summary>
	public class Article: CreativeWork
	{
		///<summary>The actual body of the article.</summary>
		public Text ArticleBody {get; set;}
		///<summary>Articles may belong to one or more 'sections' in a magazine or newspaper, such as Sports, Lifestyle, etc.</summary>
		public Text ArticleSection {get; set;}
		///<summary>The number of words in the text of the Article.</summary>
		public Integer WordCount {get; set;}
	}
}

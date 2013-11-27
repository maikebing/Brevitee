using System;

namespace Brevitee.Schema.Org
{
	///<summary>A blog</summary>
	public class Blog: CreativeWork
	{
		///<summary>A posting that is part of this blog.</summary>
		public BlogPosting BlogPost {get; set;}
		///<summary>The postings that are part of this blog (legacy spelling; see singular form, blogPost).</summary>
		public BlogPosting BlogPosts {get; set;}
	}
}

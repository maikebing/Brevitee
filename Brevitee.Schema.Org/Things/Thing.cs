using System;

namespace Brevitee.Schema.Org
{
	///<summary>The most generic type of item.</summary>
	public class Thing: DataType
	{
		///<summary>An additional type for the item, typically used for adding more specific types from external vocabularies in microdata syntax. This is a relationship between something and a class that the thing is in. In RDFa syntax, it is better to use the native RDFa syntax - the 'typeof' attribute - for multiple types. Schema.org tools may have only weaker understanding of extra types, in particular those defined externally.</summary>
		public URL AdditionalType {get; set;}
		///<summary>A short description of the item.</summary>
		public Text Description {get; set;}
		///<summary>URL of an image of the item.</summary>
		public URL Image {get; set;}
		///<summary>The name of the item.</summary>
		public Text Name {get; set;}
		///<summary>URL of the item.</summary>
		public URL Url {get; set;}
	}
}

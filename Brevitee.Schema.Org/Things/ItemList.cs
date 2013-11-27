using System;

namespace Brevitee.Schema.Org
{
	///<summary>A list of items of any sort—for example, Top 10 Movies About Weathermen, or Top 100 Party Songs. Not to be confused with HTML lists, which are often used only for formatting.</summary>
	public class ItemList: CreativeWork
	{
		///<summary>A single list item.</summary>
		public Text ItemListElement {get; set;}
		///<summary>Type of ordering (e.g. Ascending, Descending, Unordered).</summary>
		public Text ItemListOrder {get; set;}
	}
}

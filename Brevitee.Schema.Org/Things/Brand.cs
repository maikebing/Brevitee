using System;

namespace Brevitee.Schema.Org
{
	///<summary>A brand is a name used by an organization or business person for labeling a product, product group, or similar.</summary>
	public class Brand: Intangible
	{
		///<summary>URL of an image for the logo of the item.</summary>
		public ThisOrThat<ImageObject, URL> Logo {get; set;}
	}
}

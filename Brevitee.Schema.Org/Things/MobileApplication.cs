using System;

namespace Brevitee.Schema.Org
{
	///<summary>A mobile software application.</summary>
	public class MobileApplication: SoftwareApplication
	{
		///<summary>Specifies specific carrier(s) requirements for the application (e.g. an application may only work on a specific carrier network).</summary>
		public Text CarrierRequirements {get; set;}
	}
}

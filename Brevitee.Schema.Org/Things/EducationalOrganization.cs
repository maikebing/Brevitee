using System;

namespace Brevitee.Schema.Org
{
	///<summary>An educational organization.</summary>
	public class EducationalOrganization: Organization
	{
		///<summary>Alumni of educational organization.</summary>
		public Person Alumni {get; set;}
	}
}

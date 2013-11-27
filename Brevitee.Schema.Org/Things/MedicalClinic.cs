using System;

namespace Brevitee.Schema.Org
{
	///<summary>A medical clinic.</summary>
	public class MedicalClinic: MedicalOrganization
	{
		///<summary>A medical service available from this provider.</summary>
		public ThisOrThat<MedicalProcedure, MedicalTest, MedicalTherapy> AvailableService {get; set;}
		///<summary>A medical specialty of the provider.</summary>
		public MedicalSpecialty MedicalSpecialty {get; set;}
	}
}

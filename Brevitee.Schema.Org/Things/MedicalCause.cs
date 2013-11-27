using System;

namespace Brevitee.Schema.Org
{
	///<summary>The causative agent(s) that are responsible for the pathophysiologic process that eventually results in a medical condition, symptom or sign. In this schema, unless otherwise specified this is meant to be the proximate cause of the medical condition, symptom or sign. The proximate cause is defined as the causative agent that most directly results in the medical condition, symptom or sign. For example, the HIV virus could be considered a cause of AIDS. Or in a diagnostic context, if a patient fell and sustained a hip fracture and two days later sustained a pulmonary embolism which eventuated in a cardiac arrest, the cause of the cardiac arrest (the proximate cause) would be the pulmonary embolism and not the fall.</summary>
	public class MedicalCause: MedicalEntity
	{
		///<summary>The condition, complication, symptom, sign, etc. caused.</summary>
		public MedicalEntity CauseOf {get; set;}
	}
}

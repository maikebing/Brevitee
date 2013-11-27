using System;

namespace Brevitee.Schema.Org
{
	///<summary>A point value or interval for product characteristics and other purposes.</summary>
	public class QuantitativeValue: StructuredValue
	{
		///<summary>The upper of the product characteristic.</summary>
		public Number MaxValue {get; set;}
		///<summary>The lower value of the product characteristic.</summary>
		public Number MinValue {get; set;}
		///<summary>The unit of measurement given using the UN/CEFACT Common Code (3 characters).</summary>
		public Text UnitCode {get; set;}
		///<summary>The value of the product characteristic.</summary>
		public Number Value {get; set;}
		///<summary>A pointer to a secondary value that provides additional information on the original value, e.g. a reference temperature.</summary>
		public ThisOrThat<Enumeration, StructuredValue> ValueReference {get; set;}
	}
}

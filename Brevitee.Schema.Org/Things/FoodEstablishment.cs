using System;

namespace Brevitee.Schema.Org
{
	///<summary>A food-related business.</summary>
	public class FoodEstablishment: LocalBusiness
	{
		///<summary>Either Yes/No, or a URL at which reservations can be made.</summary>
		public ThisOrThat<Text, URL> AcceptsReservations {get; set;}
		///<summary>Either the actual menu or a URL of the menu.</summary>
		public ThisOrThat<Text, URL> Menu {get; set;}
		///<summary>The cuisine of the restaurant.</summary>
		public Text ServesCuisine {get; set;}
	}
}

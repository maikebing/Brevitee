using System;

namespace Brevitee.Schema.Org
{
	///<summary>An event happening at a certain time at a certain location.</summary>
	public class Event: Thing
	{
		///<summary>A person or organization attending the event.</summary>
		public ThisOrThat<Organization, Person> Attendee {get; set;}
		///<summary>A person attending the event (legacy spelling; see singular form, attendee).</summary>
		public ThisOrThat<Organization, Person> Attendees {get; set;}
		///<summary>The duration of the item (movie, audio recording, event, etc.) in ISO 8601 date format.</summary>
		public Duration Duration {get; set;}
		///<summary>The end date and time of the event (in ISO 8601 date format).</summary>
		public Date EndDate {get; set;}
		///<summary>The location of the event or organization.</summary>
		public ThisOrThat<Place, PostalAddress> Location {get; set;}
		///<summary>An offer to sell this item—for example, an offer to sell a product, the DVD of a movie, or tickets to an event.</summary>
		public Offer Offers {get; set;}
		///<summary>A performer at the event—for example, a presenter, musician, musical group or actor.</summary>
		public ThisOrThat<Organization, Person> Performer {get; set;}
		///<summary>The main performer or performers of the event—for example, a presenter, musician, or actor (legacy spelling; see singular form, performer).</summary>
		public ThisOrThat<Organization, Person> Performers {get; set;}
		///<summary>The start date and time of the event (in ISO 8601 date format).</summary>
		public Date StartDate {get; set;}
		///<summary>An Event that is part of this event. For example, a conference event includes many presentations, each are a subEvent of the conference.</summary>
		public Event SubEvent {get; set;}
		///<summary>Events that are a part of this event. For example, a conference event includes many presentations, each are subEvents of the conference (legacy spelling; see singular form, subEvent).</summary>
		public Event SubEvents {get; set;}
		///<summary>An event that this event is a part of. For example, a collection of individual music performances might each have a music festival as their superEvent.</summary>
		public Event SuperEvent {get; set;}
	}
}

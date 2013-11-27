using System;

namespace Brevitee.Schema.Org
{
	///<summary>A movie.</summary>
	public class Movie: CreativeWork
	{
		///<summary>A cast member of the movie, TV series, season, or episode, or video.</summary>
		public Person Actor {get; set;}
		///<summary>A cast member of the movie, TV series, season, or episode, or video. (legacy spelling; see singular form, actor)</summary>
		public Person Actors {get; set;}
		///<summary>The director of the movie, TV episode, or series.</summary>
		public Person Director {get; set;}
		///<summary>The duration of the item (movie, audio recording, event, etc.) in ISO 8601 date format.</summary>
		public Duration Duration {get; set;}
		///<summary>The composer of the movie or TV soundtrack.</summary>
		public ThisOrThat<MusicGroup, Person> MusicBy {get; set;}
		///<summary>The producer of the movie, TV series, season, or episode, or video.</summary>
		public Person Producer {get; set;}
		///<summary>The production company or studio that made the movie, TV series, season, or episode, or video.</summary>
		public Organization ProductionCompany {get; set;}
		///<summary>The trailer of the movie or TV series, season, or episode.</summary>
		public VideoObject Trailer {get; set;}
	}
}

using System;

namespace Brevitee.Schema.Org
{
	///<summary>A television series.</summary>
	public class TVSeries: CreativeWork
	{
		///<summary>A cast member of the movie, TV series, season, or episode, or video.</summary>
		public Person Actor {get; set;}
		///<summary>A cast member of the movie, TV series, season, or episode, or video. (legacy spelling; see singular form, actor)</summary>
		public Person Actors {get; set;}
		///<summary>The director of the movie, TV episode, or series.</summary>
		public Person Director {get; set;}
		///<summary>The end date and time of the event (in ISO 8601 date format).</summary>
		public Date EndDate {get; set;}
		///<summary>An episode of a TV series or season.</summary>
		public TVEpisode Episode {get; set;}
		///<summary>The episode of a TV series or season (legacy spelling; see singular form, episode).</summary>
		public TVEpisode Episodes {get; set;}
		///<summary>The composer of the movie or TV soundtrack.</summary>
		public ThisOrThat<MusicGroup, Person> MusicBy {get; set;}
		///<summary>The number of episodes in this season or series.</summary>
		public Number NumberOfEpisodes {get; set;}
		///<summary>The producer of the movie, TV series, season, or episode, or video.</summary>
		public Person Producer {get; set;}
		///<summary>The production company or studio that made the movie, TV series, season, or episode, or video.</summary>
		public Organization ProductionCompany {get; set;}
		///<summary>A season of a TV series.</summary>
		public TVSeason Season {get; set;}
		///<summary>The seasons of the TV series (legacy spelling; see singular form, season).</summary>
		public TVSeason Seasons {get; set;}
		///<summary>The start date and time of the event (in ISO 8601 date format).</summary>
		public Date StartDate {get; set;}
		///<summary>The trailer of the movie or TV series, season, or episode.</summary>
		public VideoObject Trailer {get; set;}
	}
}

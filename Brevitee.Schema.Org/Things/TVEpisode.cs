using System;

namespace Brevitee.Schema.Org
{
	///<summary>An episode of a TV series or season.</summary>
	public class TVEpisode: CreativeWork
	{
		///<summary>A cast member of the movie, TV series, season, or episode, or video.</summary>
		public Person Actor {get; set;}
		///<summary>A cast member of the movie, TV series, season, or episode, or video. (legacy spelling; see singular form, actor)</summary>
		public Person Actors {get; set;}
		///<summary>The director of the movie, TV episode, or series.</summary>
		public Person Director {get; set;}
		///<summary>The episode number.</summary>
		public Number EpisodeNumber {get; set;}
		///<summary>The composer of the movie or TV soundtrack.</summary>
		public ThisOrThat<MusicGroup, Person> MusicBy {get; set;}
		///<summary>The season to which this episode belongs.</summary>
		public TVSeason PartOfSeason {get; set;}
		///<summary>The TV series to which this episode or season belongs.</summary>
		public TVSeries PartOfTVSeries {get; set;}
		///<summary>The producer of the movie, TV series, season, or episode, or video.</summary>
		public Person Producer {get; set;}
		///<summary>The production company or studio that made the movie, TV series, season, or episode, or video.</summary>
		public Organization ProductionCompany {get; set;}
		///<summary>The trailer of the movie or TV series, season, or episode.</summary>
		public VideoObject Trailer {get; set;}
	}
}

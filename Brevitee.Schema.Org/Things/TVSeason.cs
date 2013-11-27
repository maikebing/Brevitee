using System;

namespace Brevitee.Schema.Org
{
	///<summary>A TV season.</summary>
	public class TVSeason: CreativeWork
	{
		///<summary>The end date and time of the event (in ISO 8601 date format).</summary>
		public Date EndDate {get; set;}
		///<summary>An episode of a TV series or season.</summary>
		public TVEpisode Episode {get; set;}
		///<summary>The episode of a TV series or season (legacy spelling; see singular form, episode).</summary>
		public TVEpisode Episodes {get; set;}
		///<summary>The number of episodes in this season or series.</summary>
		public Number NumberOfEpisodes {get; set;}
		///<summary>The TV series to which this episode or season belongs.</summary>
		public TVSeries PartOfTVSeries {get; set;}
		///<summary>The season number.</summary>
		public Integer SeasonNumber {get; set;}
		///<summary>The start date and time of the event (in ISO 8601 date format).</summary>
		public Date StartDate {get; set;}
		///<summary>The trailer of the movie or TV series, season, or episode.</summary>
		public VideoObject Trailer {get; set;}
	}
}

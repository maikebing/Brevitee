using System;

namespace Brevitee.Schema.Org
{
	///<summary>A collection of music tracks in playlist form.</summary>
	public class MusicPlaylist: CreativeWork
	{
		///<summary>The number of tracks in this album or playlist.</summary>
		public Integer NumTracks {get; set;}
		///<summary>A music recording (track)—usually a single song.</summary>
		public MusicRecording Track {get; set;}
		///<summary>A music recording (track)—usually a single song (legacy spelling; see singular form, track).</summary>
		public MusicRecording Tracks {get; set;}
	}
}

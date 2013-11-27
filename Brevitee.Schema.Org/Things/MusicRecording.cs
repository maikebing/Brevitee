using System;

namespace Brevitee.Schema.Org
{
	///<summary>A music recording (track), usually a single song.</summary>
	public class MusicRecording: CreativeWork
	{
		///<summary>The artist that performed this album or recording.</summary>
		public MusicGroup ByArtist {get; set;}
		///<summary>The duration of the item (movie, audio recording, event, etc.) in ISO 8601 date format.</summary>
		public Duration Duration {get; set;}
		///<summary>The album to which this recording belongs.</summary>
		public MusicAlbum InAlbum {get; set;}
		///<summary>The playlist to which this recording belongs.</summary>
		public MusicPlaylist InPlaylist {get; set;}
	}
}

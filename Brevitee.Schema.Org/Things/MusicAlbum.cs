using System;

namespace Brevitee.Schema.Org
{
	///<summary>A collection of music tracks.</summary>
	public class MusicAlbum: MusicPlaylist
	{
		///<summary>The artist that performed this album or recording.</summary>
		public MusicGroup ByArtist {get; set;}
	}
}

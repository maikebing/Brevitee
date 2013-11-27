using System;

namespace Brevitee.Schema.Org
{
	///<summary>A musical group, such as a band, an orchestra, or a choir. Can also be a solo musician.</summary>
	public class MusicGroup: PerformingGroup
	{
		///<summary>A music album.</summary>
		public MusicAlbum Album {get; set;}
		///<summary>A collection of music albums (legacy spelling; see singular form, album).</summary>
		public MusicAlbum Albums {get; set;}
		///<summary>A member of the music group—for example, John, Paul, George, or Ringo.</summary>
		public Person MusicGroupMember {get; set;}
		///<summary>A music recording (track)—usually a single song.</summary>
		public MusicRecording Track {get; set;}
		///<summary>A music recording (track)—usually a single song (legacy spelling; see singular form, track).</summary>
		public MusicRecording Tracks {get; set;}
	}
}

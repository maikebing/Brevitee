using System;

namespace Brevitee.Schema.Org
{
	///<summary>A video file.</summary>
	public class VideoObject: MediaObject
	{
		///<summary>The caption for this object.</summary>
		public Text Caption {get; set;}
		///<summary>The production company or studio that made the movie, TV series, season, or episode, or video.</summary>
		public Organization ProductionCompany {get; set;}
		///<summary>Thumbnail image for an image or video.</summary>
		public ImageObject Thumbnail {get; set;}
		///<summary>If this MediaObject is an AudioObject or VideoObject, the transcript of that object.</summary>
		public Text Transcript {get; set;}
		///<summary>The frame size of the video.</summary>
		public Text VideoFrameSize {get; set;}
		///<summary>The quality of the video.</summary>
		public Text VideoQuality {get; set;}
	}
}

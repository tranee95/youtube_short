using System;

namespace Common.Models.YouTobe
{
	public class ChanelSnippet
	{
		public DateTime PublishedAt { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string CustomUrl { get; set; }
		public string Country { get; set; }
		public Thumbnails thumbnails { get; set; }
	}
}

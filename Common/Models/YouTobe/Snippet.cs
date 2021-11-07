using System;

namespace Common.Models.YouTobe
{
	public class Snippet
	{
		public DateTime PublishedAt { get; set; }
		public string ChannelId { get; set; }
		public string ChannelTitle { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int CategoryId { get; set; }
		public string DefaultAudioLanguage { get; set; }
	}
}

using System.Collections.Generic;

namespace Common.Models.YouTobe
{
	public class YoutubeResponse
	{
		public string Kind { get; set; }
		public string Etag { get; set; }
		public List<YoutubeItems> Items { get; set; }
	}
}

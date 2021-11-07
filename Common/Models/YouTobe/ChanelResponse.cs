using System.Collections.Generic;

namespace Common.Models.YouTobe
{
	public class ChanelResponse
	{
		public string Kind { get; set; }
		public string Etag { get; set; }
		public List<ChanelItems> Items { get; set; }
	}
}

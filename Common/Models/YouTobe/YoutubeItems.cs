namespace Common.Models.YouTobe
{
	public class YoutubeItems
	{
		public string Id { get; set; }
		public string Kind { get; set; }
		public string Etag { get; set; }
		public Snippet Snippet { get; set; }
		public Statistics Statistics { get; set; }
	}
}

namespace Common.Models.Video
{
	public class VideoThemes
	{
		public int Id { get; set; }
		public int VideoId { get; set; }
		public int ThemeId { get; set; }
		public bool Active { get; set; }
	}
}

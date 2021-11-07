namespace Common.SqlVIewModel
{
	public class DefaultVideoView
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }
		public int BlogersId { get; set; }
		public string BlogersName { get; set; }
		public int VideoThemsId { get; set; }
		public int VideoThemsVideoId { get; set; }
		public int VideoThemsThemeId { get; set; }
		public int ThemesId { get; set; }
		public string ThemesName { get; set; }
	}
}

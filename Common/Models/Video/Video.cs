using System;

namespace Common.Models.Video
{
	public class Video
	{
		public int Id { get; set; }
		/// <summary>
		/// Тип платформы.
		/// </summary>
		public int PlatformVideoId { get; set; }
		public int BlogerId { get; set; }
		public int[] ThemesId { get; set; }
		/// <summary>
		/// Идентификатор видео на платформе.
		/// </summary>
		public string VideoId { get; set; }
		public string ChanelId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Url { get; set; }
		public string ThumbnailUrl { get; set; }
		public DateTime CreateDateTime { get; set; }
		public int StartVideoSeconds { get; set; }
		public int EndVideoSeconds { get; set; }
		public int ViewCount { get; set; }
		public int LikeCount { get; set; }
		public int DislikeCount { get; set; }
		public int CommentCount { get; set; }
		public bool Active { get; set; }
	}
}

using System.Collections.Generic;
using VideoModel = Common.Models.Video.Video;

namespace Common.ViewModels
{
	public class VideoListViewModel
	{
		public int Id { get; set; }
		public List<int> BlogersId { get; set; }
		public List<int> ThemesId { get; set; }
		public string VideoListName { get; set; }
		public string UrlAvatar { get; set; }
		public List<VideoModel> Videos { get; set; }
	}
}

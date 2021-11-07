using System.Collections.Generic;
using VideoModel = Common.Models.Video.Video;

namespace Common.ViewModels
{
	public class FavoriteViewModel
	{
		public int Id { get; set; }
		public string Guid { get; set; }
		public int UserId { get; set; }
		public string Name { get; set; }
		public List<VideoModel> Videos { get; set; }
		public bool IsDefaultList { get; set; }
		public bool Active { get; set; }
		public bool IsLock { get; set; }
	}
}

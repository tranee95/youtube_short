using Common.Models.User;
using System.Collections.Generic;
using BlogerModel = Common.Models.Bloger.Bloger;
using ThemesModel = Common.Models.Themes.Themes;
using VideoModel = Common.Models.Video.Video;


namespace Common.ViewModels
{
	public class ResultAuthenticationViewModel
	{
		public UserToken UserToken { get; set; }
		public string UserDisplay { get; set; }
		public int UserPoints { get; set; }
		public string AvatarUrl { get; set; }
		public List<BlogerModel> Blogers { get; set; }
		public List<ThemesModel> Themes { get; set; }
		public List<FavoriteViewModel> Favorite { get; set; }
		public List<VideoModel> Like { get; set; }
		public List<VideoModel> Dislike { get; set; }
	}
}

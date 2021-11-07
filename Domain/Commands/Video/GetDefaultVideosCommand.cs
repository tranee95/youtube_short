using MediatR;
using System.Collections.Generic;
using BlogerModel = Common.Models.Bloger.Bloger;
using ThemeModel = Common.Models.Themes.Themes;
using VideoModel = Common.Models.Video.Video;

namespace Domain.Commands.Video
{
	public class GetDefaultVideosCommand : IRequest<List<VideoModel>>
	{
		public List<BlogerModel> Blogers { get; set; }
		public List<ThemeModel> Themes { get; set; }
		public int Take { get; set; }
		public int Page { get; set; }
	}
}

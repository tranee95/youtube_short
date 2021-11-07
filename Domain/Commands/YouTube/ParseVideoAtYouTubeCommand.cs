using MediatR;
using System.Collections.Generic;
using ThemesModel = Common.Models.Themes.Themes;
using VideoModel = Common.Models.Video.Video;

namespace Domain.Commands.YouTube
{
	public class ParseVideoAtYouTubeCommand : IRequest<VideoModel>
	{
		public string VideoId { get; set; }
		public int StartTime { get; set; }
		public int EndTime { get; set; }
		public List<int> ThemesId { get; set; }
		public List<ThemesModel> CustomThemes { get; set; }
	}
}

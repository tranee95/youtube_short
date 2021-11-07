using MediatR;
using System.Collections.Generic;
using VideoModle = Common.Models.Video.Video;

namespace Domain.Commands.Video
{
	public class GetVideosRangeCommand : IRequest<List<VideoModle>>
	{
		public List<int> Videos { get; set; }
	}
}

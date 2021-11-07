using MediatR;
using System.Collections.Generic;
using VideoModel = Common.Models.Video.Video;

namespace Domain.Commands.Video
{
	public class GetUserLikeVideoCommand : IRequest<List<VideoModel>>
	{
		public int UserId { get; set; }
	}
}

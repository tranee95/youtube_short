using MediatR;
using System.Collections.Generic;
using VideoModel = Common.Models.Video.Video;

namespace Domain.Commands.Video
{
	public class GetUserDislikeVideoCommand : IRequest<List<VideoModel>>
	{
		public int UserId { get; set; }
	}
}

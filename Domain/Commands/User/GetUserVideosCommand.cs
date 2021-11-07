using MediatR;
using System.Collections.Generic;
using VideoModel = Common.Models.Video.Video;

namespace Domain.Commands.User
{
	public class GetUserVideosCommand : IRequest<List<VideoModel>>
	{
		public int UserId { get; set; }
		public int Take { get; set; }
		public int Page { get; set; }
	}
}

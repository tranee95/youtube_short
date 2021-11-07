using Common.ViewModels;
using MediatR;
using System.Collections.Generic;

namespace Domain.Commands.User
{
	public class GetVideoUserSubscribeBlogersCommand : IRequest<List<VideoListViewModel>>
	{
		public int CountVideo { get; set; }
		public int UserId { get; set; }
	}
}

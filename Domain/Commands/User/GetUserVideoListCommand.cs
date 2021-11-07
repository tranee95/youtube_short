using Common.ViewModels;
using MediatR;
using System.Collections.Generic;

namespace Domain.Commands.User
{
	public class GetUserVideoListCommand : IRequest<List<VideoListViewModel>>
	{
		public int CountVideo { get; set; }
		public int Page { get; set; }
		public int UserId { get; set; }
	}
}

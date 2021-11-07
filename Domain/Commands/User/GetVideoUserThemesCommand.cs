using Common.ViewModels;
using MediatR;
using System.Collections.Generic;

namespace Domain.Commands.User
{
	public class GetVideoUserThemesCommand : IRequest<List<VideoListViewModel>>
	{
		public int UserId { get; set; }
		public int VideoCount { get; set; }
	}
}

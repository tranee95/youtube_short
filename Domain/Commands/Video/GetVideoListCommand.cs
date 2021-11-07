using Common.Models.User;
using Common.ViewModels;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Commands.Video
{
	public class GetVideoListCommand : IRequest<List<VideoListViewModel>>
	{
		public List<FilterVideoModel> Filter { get; set; }
		public int CountVideo { get; set; }
		public int Page { get; set; }
	}
}

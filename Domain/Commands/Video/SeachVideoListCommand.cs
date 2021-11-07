using Common.Models.User;
using Common.ViewModels;
using MediatR;
using System.Collections.Generic;

namespace Domain.Commands.Video
{
	public class SeachVideoListCommand : IRequest<List<VideoListViewModel>>
	{
		public List<FilterVideoModel> Filter { get; set; }
		public string SearchStr { get; set; }
		public int CountVideo { get; set; }
		public int Page { get; set; }
	}
}

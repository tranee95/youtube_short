using Common.ViewModels;
using DataContext;
using Domain.Commands.Video;
using MediatR;
using Service.Video;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.Video
{
	public class SeachVideoListCommandHandler : IRequestHandler<SeachVideoListCommand, List<VideoListViewModel>>
	{
		private readonly ApplicationDbContext _context;
		private readonly IVideoService _videoService;

		public SeachVideoListCommandHandler(ApplicationDbContext context, IVideoService videoService)
		{
			_context = context;
			_videoService = videoService;
		}

		public async Task<List<VideoListViewModel>> Handle(SeachVideoListCommand request, CancellationToken cancellationToken)
		{
			return await _videoService.SearchVideoAtFilters(request.SearchStr);
		}
	}
}

using Common.ViewModels;
using DataContext;
using Domain.Commands.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Video;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.User
{
	public class GetUserVideoListCommandHandler : IRequestHandler<GetUserVideoListCommand, List<VideoListViewModel>>
	{
		private readonly ApplicationDbContext _context;
		private readonly IVideoService _videoService;

		public GetUserVideoListCommandHandler(ApplicationDbContext context, IVideoService videoService)
		{
			_context = context;
			_videoService = videoService;
		}

		public async Task<List<VideoListViewModel>> Handle(GetUserVideoListCommand request, CancellationToken cancellationToken)
		{
			var filters =
				await _context
					.UserFilterVideos
					.AsNoTracking()
					.Where(s => s.UserId.Equals(request.UserId))
					.ToListAsync(cancellationToken);

			var result = await _videoService.GetVidosAtFilters(filters);

			return result;
		}
	}
}

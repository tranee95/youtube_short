using DataContext;
using Domain.Commands.Video;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VideoModel = Common.Models.Video.Video;

namespace Domain.Handlers.Video
{
	public class GetVideosRangeCommandHandelr : IRequestHandler<GetVideosRangeCommand, List<VideoModel>>
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<GetVideosRangeCommandHandelr> _logger;

		public GetVideosRangeCommandHandelr(ApplicationDbContext context, ILogger<GetVideosRangeCommandHandelr> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task<List<VideoModel>> Handle(GetVideosRangeCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var videos =
					await _context
						.Videos
						.AsNoTracking()
						.Where(s => request.Videos.Contains(s.Id))
						.ToListAsync(cancellationToken);

				return videos;
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				_logger.LogTrace(e.StackTrace);
				return null;
			}
		}
	}
}

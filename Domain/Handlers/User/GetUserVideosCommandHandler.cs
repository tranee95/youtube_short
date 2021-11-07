using System;
using DataContext;
using Domain.Commands.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using VideoModel = Common.Models.Video.Video;


namespace Domain.Handlers.User
{
	public class GetUserVideosCommandHandler : IRequestHandler<GetUserVideosCommand, List<VideoModel>>
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<GetUserVideosCommandHandler> _logger;

		public GetUserVideosCommandHandler(ApplicationDbContext context, ILogger<GetUserVideosCommandHandler> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task<List<VideoModel>> Handle(GetUserVideosCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var skip = (request.Page - 1) * request.Take;

				var result =
					await _context
						.Videos
						.AsNoTracking()
						.Where(s => s.Active == true)
						.Skip(skip)
						.Take(request.Take)
						.ToListAsync(cancellationToken);

				return result;
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

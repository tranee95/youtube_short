using DataContext;
using Domain.Commands.Video;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VideoModel = Common.Models.Video.Video;

namespace Domain.Handlers.Video
{
	public class GetDefaultVideosHandler : IRequestHandler<GetDefaultVideosCommand, List<VideoModel>>
	{
		private readonly ApplicationDbContext _context;

		public GetDefaultVideosHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<VideoModel>> Handle(GetDefaultVideosCommand request, CancellationToken cancellationToken)
		{
			var skip = (request.Page - 1) * request.Take;
			var blogersId = request.Blogers.Select(s => s.Id).ToArray();
			var themesId = request.Themes.Select(s => s.Id).ToArray();

			var result = new List<VideoModel>();

			if (blogersId.Any() || themesId.Any())
			{
				result =
					await _context
						.Videos
						.AsNoTracking()
						.Where(s => blogersId.Contains(s.BlogerId) || s.ThemesId.Any(i => themesId.Contains(i)))
						.Skip(skip)
						.Take(request.Take)
						.Distinct()
						.ToListAsync(cancellationToken);
			}
			else
			{
				result =
					await _context
						.Videos
						.AsNoTracking()
						.Skip(skip)
						.Take(request.Take)
						.ToListAsync(cancellationToken);
			}

			return result;

		}
	}
}

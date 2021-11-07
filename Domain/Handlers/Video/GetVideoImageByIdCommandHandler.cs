using System;
using DataContext;
using Domain.Commands.Video;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.Video
{
	public class GetVideoImageByIdCommandHandler : IRequestHandler<GetVideoImageByIdCommand, string>
	{
		private readonly ApplicationDbContext _context;

		public GetVideoImageByIdCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<string> Handle(GetVideoImageByIdCommand request, CancellationToken cancellationToken)
		{
			var video = await _context
				.Videos
				.AsNoTracking()
				.FirstOrDefaultAsync(s => s.Id.Equals(request.Id), cancellationToken);

			if (video == null)
				throw new Exception($"Видео {request.Id} не найдено");
			
			return video.ThumbnailUrl;
		}
	}
}

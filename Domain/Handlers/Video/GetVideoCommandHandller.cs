using DataContext;
using Domain.Commands.Video;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VideoModel = Common.Models.Video.Video;

namespace Domain.Handlers.Video
{
	public class GetVideoCommandHandller : IRequestHandler<GetVideoCommand, VideoModel>
	{
		private readonly ApplicationDbContext _context;

		public GetVideoCommandHandller(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<VideoModel> Handle(GetVideoCommand request, CancellationToken cancellationToken)
		{
			return await _context
					.Videos
					.AsNoTracking()
					.FirstOrDefaultAsync(s => s.Id == request.VideoId, cancellationToken);
		}
	}
}

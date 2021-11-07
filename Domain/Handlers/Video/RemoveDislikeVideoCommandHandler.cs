using Common.Models.Video;
using DataContext;
using Domain.Commands.Video;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.Video
{
	public class RemoveDislikeVideoCommandHandler : IRequestHandler<RemoveDislikeVideoCommand, bool>
	{
		private readonly ApplicationDbContext _context;

		public RemoveDislikeVideoCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(RemoveDislikeVideoCommand request, CancellationToken cancellationToken)
		{
			var video =
				await _context
					.Videos
					.FirstOrDefaultAsync(s => s.Id.Equals(request.VideoId), cancellationToken);

			if (request.UserId != null)
			{
				var dislike =
					await _context
						.Dislikes
						.FirstOrDefaultAsync(s => s.VideoId.Equals(request.VideoId) && s.UserId.Equals(request.UserId.Value),
							cancellationToken);

				if (dislike != null) await RemoveUserDislike(dislike);
			}

			if (video is null) return false;

			video.DislikeCount -= 1;
			await _context.SaveChangesAsync(cancellationToken);

			return true;
		}

		private async Task RemoveUserDislike(Dislikes dislike)
		{
			_context.Dislikes.Remove(dislike);
			await _context.SaveChangesAsync();
		}
	}
}

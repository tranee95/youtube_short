using Common.Models.Video;
using DataContext;
using Domain.Commands.Video;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.Video
{
	public class RemoveLikeVideoCommandHandler : IRequestHandler<RemoveLikeVideoCommand, bool>
	{
		private readonly ApplicationDbContext _context;

		public RemoveLikeVideoCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(RemoveLikeVideoCommand request, CancellationToken cancellationToken)
		{
			var video =
				await _context
					.Videos
					.FirstOrDefaultAsync(s => s.Id.Equals(request.VideoId), cancellationToken);

			if (request.UserId != null)
			{
				var like =
					await _context
						.Likes
						.FirstOrDefaultAsync(s => s.VideoId.Equals(request.VideoId) && s.UserId.Equals(request.UserId.Value),
							cancellationToken);

				if (like != null) await RemoveUserLike(like);
			}

			if (video is null) return false;

			video.LikeCount -= 1;
			await _context.SaveChangesAsync(cancellationToken);

			return true;
		}

		private async Task RemoveUserLike(Likes like)
		{
			_context.Likes.Remove(like);
			await _context.SaveChangesAsync();
		}
	}
}

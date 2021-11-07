using Common.Models.Video;
using DataContext;
using Domain.Commands.Video;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.Video
{
	public class AddDislikeCommandHandler : IRequestHandler<AddDislikeCommand, bool>
	{
		private readonly ApplicationDbContext _context;

		public AddDislikeCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(AddDislikeCommand request, CancellationToken cancellationToken)
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

				if (dislike is null) await AddUserDislike(request.UserId.Value, request.VideoId);
			}

			if (video is null) return false;

			video.DislikeCount += 1;
			await _context.SaveChangesAsync(cancellationToken);

			return true;
		}

		private async Task AddUserDislike(int userId, int videoId)
		{
			await _context
				.Dislikes
				.AddAsync(new Dislikes
				{
					UserId = userId,
					VideoId = videoId,
					Active = true
				});

			await _context.SaveChangesAsync();
		}
	}
}

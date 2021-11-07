using Common.Models.Video;
using DataContext;
using Domain.Commands.Video;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.Video
{
	public class AddLikeVideoCommandHandler : IRequestHandler<AddLikeVideoCommand, bool>
	{
		private readonly ApplicationDbContext _context;

		public AddLikeVideoCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(AddLikeVideoCommand request, CancellationToken cancellationToken)
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

				if (like is null) await AddUserLike(request.UserId.Value, request.VideoId);
			}

			if (video is null) return false;

			video.LikeCount += 1;
			await _context.SaveChangesAsync(cancellationToken);

			return true;
		}

		private async Task AddUserLike(int userId, int videoId)
		{
			await _context
				.Likes
				.AddAsync(new Likes
				{
					UserId = userId,
					VideoId = videoId,
					Active = true
				});

			await _context.SaveChangesAsync();
		}
	}
}

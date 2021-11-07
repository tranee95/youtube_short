using DataContext;
using Domain.Commands.Video;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VideoModel = Common.Models.Video.Video;

namespace Domain.Handlers.Video
{
	public class GetUserDislikeVideoCommandHandler : IRequestHandler<GetUserDislikeVideoCommand, List<VideoModel>>
	{
		private readonly ApplicationDbContext _context;

		public GetUserDislikeVideoCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<VideoModel>> Handle(GetUserDislikeVideoCommand request, CancellationToken cancellationToken)
		{
			var result =
				await _context
					.Dislikes
					.Where(s => s.UserId.Equals(request.UserId))
					.Join(_context.Videos,
						userDislike => userDislike.VideoId,
						video => video.Id,
						(userLike, video) => video)
					.ToListAsync(cancellationToken);

			return result;
		}
	}
}

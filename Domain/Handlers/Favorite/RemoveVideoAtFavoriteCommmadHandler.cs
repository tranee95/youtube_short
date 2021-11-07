using DataContext;
using Domain.Commands.Favorite;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.Favorite
{
	public class RemoveVideoAtFavoriteCommmadHandler : IRequestHandler<RemoveVideoAtFavoriteCommmad, bool>
	{
		private readonly ApplicationDbContext _context;

		public RemoveVideoAtFavoriteCommmadHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(RemoveVideoAtFavoriteCommmad request, CancellationToken cancellationToken)
		{
			try
			{
				var favorite =
					await _context
						.Favorite
						.FirstOrDefaultAsync(s => s.Id.Equals(request.Id) ||
												  (s.Guid.Equals(request.Guid) && !string.IsNullOrEmpty(request.Guid)),
							cancellationToken);

				var videosId = favorite.VideosId.ToList();
				var index = videosId.FindIndex(s => s.Equals(request.VideoId));

				if (index <= -1) return false;

				videosId.RemoveAt(index);
				favorite.VideosId = videosId.ToArray();
				await _context.SaveChangesAsync(cancellationToken);

				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}

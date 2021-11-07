using DataContext;
using Domain.Commands.Favorite;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.Favorite
{
	public class AddVideoToDefaultFavoriteCommandHandler : IRequestHandler<AddVideoToDefaultFavoriteCommand, bool>
	{
		private readonly ApplicationDbContext _context;

		public AddVideoToDefaultFavoriteCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(AddVideoToDefaultFavoriteCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var f =
					await _context
						.Favorite
						.FirstOrDefaultAsync(s => s.IsDefaultList == true && s.UserId.Equals(request.UserId),
							cancellationToken);

				var videosId = f.VideosId.ToList();
				videosId.Add(request.VideoId);

				f.VideosId = videosId.ToArray();

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

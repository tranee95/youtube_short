using System.Linq;
using DataContext;
using Domain.Commands.Favorite;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Common.ViewModels;

namespace Domain.Handlers.Favorite
{
	public class GetFavoriteCommandHandler : IRequestHandler<GetFavoriteCommand, FavoriteViewModel>
	{
		private readonly ApplicationDbContext _context;

		public GetFavoriteCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<FavoriteViewModel> Handle(GetFavoriteCommand request, CancellationToken cancellationToken)
		{
			var favorite =
				await _context
					.Favorite
					.AsNoTracking()
					.FirstOrDefaultAsync(s => s.Id.Equals(request.Id) ||
					                          (s.Guid.Equals(request.Guid) && !string.IsNullOrEmpty(request.Guid)),
						cancellationToken);

			var videos =
				_context
					.Videos
					.AsNoTracking()
					.Where(s => favorite.VideosId.Contains(s.Id))
					.ToList();

			return new FavoriteViewModel
			{
				Id = favorite.Id,
				Guid = favorite.Guid,
				UserId = favorite.UserId,
				Name = favorite.Name,
				IsDefaultList = favorite.IsDefaultList,
				Videos = videos,
				Active = favorite.Active
			};
		}
	}
}

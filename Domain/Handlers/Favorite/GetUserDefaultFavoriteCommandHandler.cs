using Common.ViewModels;
using DataContext;
using Domain.Commands.Favorite;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.Favorite
{
	public class GetUserDefaultFavoriteCommandHandler : IRequestHandler<GetUserDefaultFavoriteCommand, FavoriteViewModel>
	{
		private readonly ApplicationDbContext _context;

		public GetUserDefaultFavoriteCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<FavoriteViewModel> Handle(GetUserDefaultFavoriteCommand request, CancellationToken cancellationToken)
		{
			var favorite =
			 	await _context
						.Favorite
						.AsNoTracking()
						.FirstOrDefaultAsync(s => s.UserId.Equals(request.Id) && s.IsDefaultList == true,
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

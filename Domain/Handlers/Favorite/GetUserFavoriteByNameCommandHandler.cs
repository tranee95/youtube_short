using DataContext;
using Domain.Commands.Favorite;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using FavoriteModel = Common.Models.Favorite.Favorite;

namespace Domain.Handlers.Favorite
{
	public class GetUserFavoriteByNameCommandHandler : IRequestHandler<GetUserFavoriteByNameCommand, FavoriteModel>
	{
		private readonly ApplicationDbContext _context;

		public GetUserFavoriteByNameCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<FavoriteModel> Handle(GetUserFavoriteByNameCommand request, CancellationToken cancellationToken)
		{
			var f =
				await _context
					.Favorite
					.FirstOrDefaultAsync(
						s => s.UserId.Equals(request.UserId) &&
							 s.Name.ToLower().Contains(request.FavoriteName.ToLower()),
						cancellationToken);

			return f;
		}
	}
}

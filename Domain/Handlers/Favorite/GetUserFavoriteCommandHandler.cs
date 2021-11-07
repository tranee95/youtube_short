using Common.ViewModels;
using DataContext;
using Domain.Commands.Favorite;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.Favorite
{
	public class GetUserFavoriteCommandHandler : IRequestHandler<GetUserFavoriteCommand, List<FavoriteViewModel>>
	{
		private readonly ApplicationDbContext _context;

		public GetUserFavoriteCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<FavoriteViewModel>> Handle(GetUserFavoriteCommand request, CancellationToken cancellationToken)
		{
			var result =
				await _context
					.Favorite
					.AsNoTracking()
					.Where(s => s.UserId.Equals(request.UserId))
					.Select(favorite => new FavoriteViewModel
					{
						Id = favorite.Id,
						Guid = favorite.Guid,
						UserId = favorite.UserId,
						Name = favorite.Name,
						IsDefaultList = favorite.IsDefaultList,
						Active = favorite.Active,
						IsLock = favorite.IsLock,
						Videos = _context
							.Videos
							.AsNoTracking()
							.Where(s => favorite.VideosId.Contains(s.Id))
							.ToList()

					})
					.ToListAsync(cancellationToken);

			return result;
		}
	}
}

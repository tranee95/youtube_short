using DataContext;
using Domain.Commands.Favorite;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using FavoriteModel = Common.Models.Favorite.Favorite;

namespace Domain.Handlers.Favorite
{
	public class CreateFavoriteCommandhandler : IRequestHandler<CreateFavoriteCommand, FavoriteModel>
	{
		private readonly ApplicationDbContext _context;

		public CreateFavoriteCommandhandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<FavoriteModel> Handle(CreateFavoriteCommand request, CancellationToken cancellationToken)
		{
			var favorite = new FavoriteModel
			{
				Guid = request.Guid,
				IsDefaultList = request.IsDefault,
				Name = request.Name,
				VideosId = new int[] { },
				UserId = request.UserId,
				IsLock = request.IsLock,
				Active = true
			};

			await _context.Favorite.AddAsync(favorite, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);

			return favorite;
		}
	}
}

using DataContext;
using Domain.Commands.Favorite;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using FavoriteModel = Common.Models.Favorite.Favorite;


namespace Domain.Handlers.Favorite
{
	public class CreateDefaultFavoriteCommandHandler : IRequestHandler<CreateDefaultFavoriteCommand, FavoriteModel>
	{
		private readonly ApplicationDbContext _context;

		public CreateDefaultFavoriteCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<FavoriteModel> Handle(CreateDefaultFavoriteCommand request, CancellationToken cancellationToken)
		{
			var f = new FavoriteModel
			{
				IsDefaultList = true,
				Name = "My favorite videos",
				VideosId = default(int).Equals(request.VideoId) ? new int[] { } : new[] { request.VideoId },
				UserId = request.UserId,
				Active = true
			};

			await _context
				.Favorite
				.AddAsync(f, cancellationToken);

			await _context.SaveChangesAsync(cancellationToken);
			return f;
		}
	}
}

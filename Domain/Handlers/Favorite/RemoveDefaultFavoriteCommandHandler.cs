using DataContext;
using Domain.Commands.Favorite;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.Favorite
{
	public class RemoveDefaultFavoriteCommandHandler : IRequestHandler<RemoveDefaultFavoriteCommand, bool>
	{
		private readonly ApplicationDbContext _context;

		public RemoveDefaultFavoriteCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(RemoveDefaultFavoriteCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var f =
					await _context
						.Favorite
						.FirstOrDefaultAsync(s => s.IsDefaultList == true && s.UserId.Equals(request.UserId),
							cancellationToken);

				if (f != null)
				{
					_context.Favorite.Remove(f);
					await _context.SaveChangesAsync(cancellationToken);
				}

				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}

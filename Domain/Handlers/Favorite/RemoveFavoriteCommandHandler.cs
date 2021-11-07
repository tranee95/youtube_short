using DataContext;
using Domain.Commands.Favorite;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.Favorite
{
	public class RemoveFavoriteCommandHandler : IRequestHandler<RemoveFavoriteCommand, bool>
	{
		private readonly ApplicationDbContext _context;

		public RemoveFavoriteCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(RemoveFavoriteCommand request, CancellationToken cancellationToken)
		{
			try
			{ 
				var f =
					await _context
						.Favorite
						.FirstOrDefaultAsync(s => s.Id.Equals(request.Id) ||
						                          (s.Guid.Equals(request.Guid) && !string.IsNullOrEmpty(request.Guid)),
							cancellationToken);

				_context.Favorite.Remove(f);
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

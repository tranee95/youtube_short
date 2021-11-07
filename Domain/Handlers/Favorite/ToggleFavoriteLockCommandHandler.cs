using DataContext;
using Domain.Commands.Favorite;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.Favorite
{
	public class ToggleFavoriteLockCommandHandler : IRequestHandler<ToggleFavoriteLockCommand, bool>
	{
		private readonly ApplicationDbContext _context;

		public ToggleFavoriteLockCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(ToggleFavoriteLockCommand request, CancellationToken cancellationToken)
		{
			var f =
				await _context
					.Favorite
					.FirstOrDefaultAsync(s => s.Guid.Equals(request.Guid), cancellationToken);

			f.IsLock = !f.IsLock;

			await _context.SaveChangesAsync(cancellationToken);
			return f.IsLock;
		}
	}
}

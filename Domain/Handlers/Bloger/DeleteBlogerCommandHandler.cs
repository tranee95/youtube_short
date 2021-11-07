using System.Threading;
using System.Threading.Tasks;
using DataContext;
using Domain.Commands.Bloger;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Handlers.Bloger
{
	public class DeleteBlogerCommandHandler : IRequestHandler<DeleteBlogerCommand, bool>
	{
		private readonly ApplicationDbContext _context;

		public DeleteBlogerCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(DeleteBlogerCommand request, CancellationToken cancellationToken)
		{
			var bloger = await _context
			                   .Blogers
			                   .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

			if (bloger != null)
			{
				_context.Blogers.Remove(bloger);
				await _context.SaveChangesAsync(cancellationToken);

				return true;
			}

			return false;
		}
	}
}
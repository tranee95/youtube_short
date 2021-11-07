using DataContext;
using Domain.Commands.User;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.User
{
	public class DeteleUserSubscrabeToBolgerCommandHandler : IRequestHandler<DeteleUserSubscrabeToBolgerCommand, bool>
	{
		private readonly ApplicationDbContext _context;

		public DeteleUserSubscrabeToBolgerCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(DeteleUserSubscrabeToBolgerCommand request, CancellationToken cancellationToken)
		{
			var subscribe =
				_context
					.UserBloger
					.Where(s => s.UserId.Equals(request.UserId) && s.BlogerId.Equals(request.BlogerId));

			_context.RemoveRange(subscribe);
			await _context.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}

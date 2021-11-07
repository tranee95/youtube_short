using Common.Models.User;
using DataContext;
using Domain.Commands.User;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.User
{
	public class UserSubscribeToBolgerCommandHandler : IRequestHandler<UserSubscribeToBolgerCommand, bool>
	{
		private readonly ApplicationDbContext _context;

		public UserSubscribeToBolgerCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(UserSubscribeToBolgerCommand request, CancellationToken cancellationToken)
		{
			var subscrabe =
				request
					.BlogersId
					.Select(blogerId => new UserBloger
					{
						UserId = request.UserId,
						BlogerId = blogerId,
						Active = true
					});

			await _context.AddRangeAsync(subscrabe, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}

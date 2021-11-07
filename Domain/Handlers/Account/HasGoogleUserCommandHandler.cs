using DataContext;
using Domain.Commands.Account;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.Account
{
	public class HasGoogleUserCommandHandler : IRequestHandler<HasGoogleUserCommand, bool>
	{
		private readonly ApplicationDbContext _context;

		public HasGoogleUserCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(HasGoogleUserCommand request, CancellationToken cancellationToken)
		{
			var user =
				await _context
					.Users
					.AsNoTracking()
					.FirstOrDefaultAsync(s => s.GoogleId.Equals(request.GoogleUserId),
						cancellationToken);

			return !(user is null);
		}
	}
}

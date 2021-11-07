using DataContext;
using Domain.Commands.Account;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.Account
{
	public class GetUserIdCommandHandler : IRequestHandler<GetUserIdCommand, int>
	{
		private readonly ApplicationDbContext _context;

		public GetUserIdCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<int> Handle(GetUserIdCommand request, CancellationToken cancellationToken)
		{
			var user =
				await _context
					.Users
					.AsNoTracking()
					.FirstOrDefaultAsync(s => s.Email.Equals(request.Email),
						cancellationToken);

			return user?.Id ?? 0;
		}
	}
}

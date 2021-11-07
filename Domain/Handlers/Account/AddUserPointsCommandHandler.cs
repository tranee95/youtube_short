using DataContext;
using Domain.Commands.Account;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.Account
{
	public class AddUserPointsCommandHandler : IRequestHandler<AddUserPointsCommand, int>
	{
		private readonly ApplicationDbContext _context;

		public AddUserPointsCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<int> Handle(AddUserPointsCommand request, CancellationToken cancellationToken)
		{
			var user =
				await _context
					.Users
					.FirstOrDefaultAsync(s => s.Id.Equals(request.UserId),
						cancellationToken);

			user.Points += request.Points;
			await _context.SaveChangesAsync(cancellationToken);

			return user.Points;
		}
	}
}

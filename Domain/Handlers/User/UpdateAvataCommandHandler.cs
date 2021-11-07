using System.Threading;
using System.Threading.Tasks;
using DataContext;
using Domain.Commands.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Handlers.User
{
	public class UpdateAvataCommandHandler : IRequestHandler<UpdateAvataCommand, bool>
	{
		private readonly ApplicationDbContext _context;

		public UpdateAvataCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(UpdateAvataCommand request, CancellationToken cancellationToken)
		{
			var user = await _context.Users.FirstOrDefaultAsync(s => s.Id == request.UserId, cancellationToken);

			if (user is null) return false;

			user.Avatar = request.Avatar;
			await _context.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}
using DataContext;
using Domain.Commands.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.User
{
	public class DeleteUserThemesCommandHandler : IRequestHandler<DeleteUsersThemesCommand, bool>
	{
		private readonly ApplicationDbContext _context;

		public DeleteUserThemesCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(DeleteUsersThemesCommand request, CancellationToken cancellationToken)
		{
			var userTheme =
				await _context
					.UserThemes
					.FirstOrDefaultAsync(s => s.UserId == request.UserId && s.ThemeId == request.ThemeId,
						cancellationToken: cancellationToken);

			if (userTheme is null) return false;

			_context.Remove(userTheme);
			await _context.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}

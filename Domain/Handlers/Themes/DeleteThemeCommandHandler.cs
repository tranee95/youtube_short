using System.Threading;
using System.Threading.Tasks;
using DataContext;
using Domain.Commands.Themes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Handlers.Themes
{
	public class DeleteThemeCommandHandler : IRequestHandler<DeleteThemeCommand, bool>
	{
		private readonly ApplicationDbContext _context;

		public DeleteThemeCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(DeleteThemeCommand request, CancellationToken cancellationToken)
		{
			var theme =
				await _context
					.Themes
					.FirstOrDefaultAsync(s => s.Id == request.ThemeId, cancellationToken);

			if (theme is null) return false;

			_context.Themes.Remove(theme);
			await _context.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}
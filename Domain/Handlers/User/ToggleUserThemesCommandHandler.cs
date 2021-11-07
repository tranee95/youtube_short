using Common.Models.User;
using DataContext;
using Domain.Commands.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.User
{
	public class ToggleUserThemesCommandHandler : IRequestHandler<ToggleUserThemesCommand, bool>
	{
		private readonly ApplicationDbContext _context;

		public ToggleUserThemesCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(ToggleUserThemesCommand request, CancellationToken cancellationToken)
		{
			var themes =
				await _context
					.UserThemes
					.AsNoTracking()
					.FirstOrDefaultAsync(s => s.UserId.Equals(request.UserId) && s.ThemeId.Equals(request.ThemesId), cancellationToken);

			if (themes == null)
			{
				await _context.UserThemes.AddAsync(new UserThemes
				{
					UserId = request.UserId,
					ThemeId = request.ThemesId,
					Active = true
				}, cancellationToken);
			}

			if (themes != null)
			{
				_context.UserThemes.Remove(themes);
			}

			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
	}
}

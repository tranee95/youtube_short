using DataContext;
using Domain.Commands.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ThemesModel = Common.Models.Themes.Themes;

namespace Domain.Handlers.User
{
	public class GetUserThemesCommandHandler : IRequestHandler<GetUserThemesCommand, List<ThemesModel>>
	{
		private readonly ApplicationDbContext _context;

		public GetUserThemesCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<ThemesModel>> Handle(GetUserThemesCommand request, CancellationToken cancellationToken)
		{
			var result =
				await _context
					.UserThemes
					.AsNoTracking()
					.Where(s => s.UserId == request.UserId)
					.Join(_context.Themes,
						userThemes => userThemes.ThemeId,
						theme => theme.Id,
						(userThemes, theme) => theme)
					.ToListAsync(cancellationToken);

			return result;
		}
	}
}

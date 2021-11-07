using System.Threading;
using System.Threading.Tasks;
using DataContext;
using Domain.Commands.Themes;
using MediatR;
using ThemesModel = Common.Models.Themes.Themes;

namespace Domain.Handlers.Themes
{
	public class AddThemesCommandHandler : IRequestHandler<AddThemeCommand, ThemesModel>
	{
		private readonly ApplicationDbContext _context;

		public AddThemesCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<ThemesModel> Handle(AddThemeCommand request, CancellationToken cancellationToken)
		{
			var theme = new ThemesModel {Name = request.Name, Active = true};

			await _context.Themes.AddAsync(theme, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);

			return theme;
		}
	}
}
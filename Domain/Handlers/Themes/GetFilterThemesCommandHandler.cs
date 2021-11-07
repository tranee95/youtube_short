using DataContext;
using Domain.Commands.Themes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ThemesModel = Common.Models.Themes.Themes;

namespace Domain.Handlers.Themes
{
	public class GetFilterThemesCommandHandler : IRequestHandler<GetFilterThemesCommand, List<ThemesModel>>
	{
		private readonly ApplicationDbContext _context;

		public GetFilterThemesCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<ThemesModel>> Handle(GetFilterThemesCommand request, CancellationToken cancellationToken)
		{
			var themes =
				_context
					.Themes
					.AsNoTracking()
					.Where(s => s.Active == true);

			if (!string.IsNullOrEmpty(request.ThemesName))
			{
				themes = themes.Where(s => EF.Functions.Like(s.Name.ToLower(), $"%{request.ThemesName.ToLower()}%"));
			}

			return await themes.OrderBy(s => s.Id).ToListAsync(cancellationToken);
		}
	}
}

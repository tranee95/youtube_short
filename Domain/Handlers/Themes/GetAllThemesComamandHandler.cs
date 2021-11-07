using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataContext;
using Domain.Commands.Themes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ThemesModel = Common.Models.Themes.Themes;

namespace Domain.Handlers.Themes
{
	public class GetAllThemesComamandHandler : IRequestHandler<GetAllThemesComamand, List<ThemesModel>>
	{
		private readonly ApplicationDbContext _context;

		public GetAllThemesComamandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<ThemesModel>> Handle(GetAllThemesComamand request, CancellationToken cancellationToken)
		{ return await _context
				.Themes
				.AsNoTracking()
				.Where(s => s.Active)
				.ToListAsync(cancellationToken);
		}
	}
}
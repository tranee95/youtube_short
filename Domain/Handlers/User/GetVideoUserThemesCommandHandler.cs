using Common.ViewModels;
using DataContext;
using Domain.Commands.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.User
{
	public class GetVideoUserThemesCommandHandler : IRequestHandler<GetVideoUserThemesCommand, List<VideoListViewModel>>
	{
		private readonly ApplicationDbContext _context;

		public GetVideoUserThemesCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<VideoListViewModel>> Handle(GetVideoUserThemesCommand request, CancellationToken cancellationToken)
		{
			var result = new List<VideoListViewModel>();

			var themes =
				_context
					.UserThemes
					.AsNoTracking()
					.Where(s => s.UserId.Equals(request.UserId))
					.Join(_context.Themes,
						userThemes => userThemes.ThemeId,
						theme => theme.Id,
						(userThemes, theme) => theme);

			foreach (var item in themes)
			{
				var model = new VideoListViewModel
				{
					VideoListName = item.Name,
					UrlAvatar = string.Empty,
					Videos = await _context
						.Videos
						.Where(s => s.ThemesId.Contains(item.Id))
						.Take(request.VideoCount)
						.ToListAsync(cancellationToken)
				};

				result.Add(model);
			}

			return result;
		}
	}
}

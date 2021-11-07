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
	public class GetVideoUserSubscribeBlogersCommandHandler : IRequestHandler<GetVideoUserSubscribeBlogersCommand, List<VideoListViewModel>>
	{
		private readonly ApplicationDbContext _context;

		public GetVideoUserSubscribeBlogersCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<VideoListViewModel>> Handle(GetVideoUserSubscribeBlogersCommand request, CancellationToken cancellationToken)
		{
			var result = new List<VideoListViewModel>();

			var blogers =
				await _context
				.UserBloger
				.AsNoTracking()
				.Where(s => s.UserId.Equals(request.UserId))
				.Join(_context.Blogers,
					userBloger => userBloger.BlogerId,
					bloger => bloger.Id,
					(userBloger, bloger) => bloger)
				.ToListAsync(cancellationToken);

			foreach (var item in blogers)
			{
				var model = new VideoListViewModel
				{
					VideoListName = item.Name,
					UrlAvatar = item.Url,
					Videos = await _context
						.Videos
						.AsNoTracking()
						.Where(x => x.BlogerId.Equals(item.Id))
						.Take(request.CountVideo)
						.ToListAsync(cancellationToken)
				};

				result.Add(model);
			}

			return result;
		}
	}
}

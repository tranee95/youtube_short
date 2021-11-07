using System;
using DataContext;
using Domain.Commands.Favorite;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.Favorite
{
	public class RemoveVideoAtDefaultFavoriteCommandHandler : IRequestHandler<RemoveVideoAtDefaultFavoriteCommand, bool>
	{
		private readonly ApplicationDbContext _context;

		public RemoveVideoAtDefaultFavoriteCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(RemoveVideoAtDefaultFavoriteCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var f =
					await _context
						.Favorite
						.FirstOrDefaultAsync(s => s.IsDefaultList == true && s.UserId.Equals(request.UserId),
							cancellationToken);

				var videosId = f.VideosId.ToList();
				var index = videosId.FindIndex(s => s.Equals(request.VideoId));

				if (index <= -1) return false;
				
				videosId.RemoveAt(index);
				f.VideosId = videosId.ToArray();
				await _context.SaveChangesAsync(cancellationToken);

				return true;

			}
			catch (Exception e)
			{
				return false;
			}
		}
	}
}

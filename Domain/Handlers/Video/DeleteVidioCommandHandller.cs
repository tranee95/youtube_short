using DataContext;
using Domain.Commands.Video;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.Video
{
	public class DeleteVidioCommandHandller : IRequestHandler<DeleteVidioCommand, bool>
	{
		private readonly ApplicationDbContext _context;

		public DeleteVidioCommandHandller(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(DeleteVidioCommand request, CancellationToken cancellationToken)
		{
			var video =
			   await _context
					.Videos
					.FirstOrDefaultAsync(s => s.Id.Equals(request.VideoId), cancellationToken);

			if (video is null) return false;

			_context.Remove(video);
			await _context.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}

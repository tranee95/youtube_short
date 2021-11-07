using DataContext;
using Domain.Commands.Video;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using VideoModel = Common.Models.Video.Video;

namespace Domain.Handlers.Video
{
	public class AddVideoCommandHandller : IRequestHandler<AddVideoCommand, bool>
	{
		private readonly ApplicationDbContext _context;

		public AddVideoCommandHandller(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(AddVideoCommand request, CancellationToken cancellationToken)
		{
			var video = new VideoModel
			{
				PlatformVideoId = request.PlatformVideoId,
				BlogerId = request.BlogerId,
				ThemesId = request.ThemesId.ToArray(),
				ChanelId = request.ChanelId,
				CommentCount = request.CommentCount,
				CreateDateTime = DateTime.Now,
				Url = request.Url,
				Name = request.Name,
				Description = request.Description,
				DislikeCount = request.DislikeCount,
				LikeCount = request.LikeCount,
				ViewCount = request.ViewCount,
				StartVideoSeconds = request.StartVideoSeconds,
				EndVideoSeconds = request.EndVideoSeconds,
				Active = true
			};

			await _context.Videos.AddAsync(video, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}

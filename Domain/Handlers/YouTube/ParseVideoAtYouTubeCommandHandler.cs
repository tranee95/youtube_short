using System.Linq;
using DataContext;
using Domain.Commands.YouTube;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service.YouTube;
using System.Threading;
using System.Threading.Tasks;
using Common.Enums;
using Common.Extensions;
using BlogerModel = Common.Models.Bloger.Bloger;
using VideoModel = Common.Models.Video.Video;
using ThemesModel = Common.Models.Themes.Themes;

namespace Domain.Handlers.YouTube
{
	public class ParseVideoAtYouTubeCommandHandler : IRequestHandler<ParseVideoAtYouTubeCommand, VideoModel>
	{
		private readonly ApplicationDbContext _context;
		private readonly YouTubeService _youTubeService;
		private readonly ILogger<ParseVideoAtYouTubeCommandHandler> _logger;

		public ParseVideoAtYouTubeCommandHandler(ApplicationDbContext context, YouTubeService youTubeService, ILogger<ParseVideoAtYouTubeCommandHandler> logger)
		{
			_context = context;
			_youTubeService = youTubeService;
			_logger = logger;
		}

		public async Task<VideoModel> Handle(ParseVideoAtYouTubeCommand request, CancellationToken cancellationToken)
		{
			var response = await _youTubeService.GetVideoAsync(request.VideoId);
			if (response.Items.IsNullOrEmpty())
			{
				return new VideoModel();
			}

			var item = response.Items.First();
			var bloger = await FindeChanel(item.Snippet.ChannelId);

			var video = new VideoModel
			{
				PlatformVideoId = (int) VideoPlatforms.Youtube,
				Name = item.Snippet.Title,
				Description = item.Snippet.Description,
				BlogerId = bloger?.Id ?? await AddBloger(item.Snippet.ChannelId),
				ChanelId = item.Snippet.ChannelId,
				VideoId = item.Id,
				Url = $"https://www.youtube.com/watch?v={item.Id}",
				ThumbnailUrl = $"https://img.youtube.com/vi/{item.Id}/0.jpg",
				CreateDateTime = item.Snippet.PublishedAt,
				StartVideoSeconds = request.StartTime,
				EndVideoSeconds = request.EndTime,
				LikeCount = item.Statistics.LikeCount,
				DislikeCount = item.Statistics.DislikeCount,
				CommentCount = item.Statistics.CommentCount,
				ViewCount = item.Statistics.ViewCount,
				ThemesId = request.ThemesId.ToArray(),
				Active = true,
			};

			if (request.CustomThemes.Any())
			{
				var customThemesesId = await AddCustomThemes(request.CustomThemes.ToArray());
				video.ThemesId = video.ThemesId.Concat(customThemesesId).ToArray();
			}

			await _context.Videos.AddAsync(video, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);

			_logger.LogWarning($"add video Id: {video.Id}");

			return video;
		}

		private async Task<int[]> AddCustomThemes(ThemesModel[] customThemes)
		{
			using (var tr = await _context.Database.BeginTransactionAsync())
			{
				await _context.Themes.AddRangeAsync(customThemes);
				await _context.SaveChangesAsync();
				await tr.CommitAsync();

				return customThemes.Select(s => s.Id).ToArray();
			}
		}

		private async Task<BlogerModel> FindeChanel(string chanelId)
		{
			var b =
				await _context
					.Blogers
					.AsNoTracking()
					.FirstOrDefaultAsync(s => s.ChanelId.Equals(chanelId));

			return b;
		}

		private async Task<int> AddBloger(string chanelId)
		{
			var chanel = await _youTubeService.GetChanelAsync(chanelId);
			var bloger = new BlogerModel
			{
				ChanelId = chanel.Items[0].Id,
				Name = chanel.Items[0].Snippet.Title,
				NikcName = chanel.Items[0].Snippet.Title,
				Url = $"https://www.youtube.com/user/{chanel.Items[0].Snippet.CustomUrl}",
				Avatar = null,
				UrlAvatar = chanel.Items[0].Snippet.thumbnails.Default.Url,
				Active = true
			};

			await _context.Blogers.AddAsync(bloger);
			await _context.SaveChangesAsync();

			_logger.LogWarning($"add video Id: {bloger.Id}");

			return bloger.Id;
		}
	}
}

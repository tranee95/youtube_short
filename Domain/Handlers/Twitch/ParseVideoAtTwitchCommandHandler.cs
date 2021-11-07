using System;
using System.Linq;
using DataContext;
using Domain.Commands.Twitch;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service.Twitch;
using System.Threading;
using System.Threading.Tasks;
using Common.Enums;
using Common.Extensions;
using BlogerModel = Common.Models.Bloger.Bloger;
using VideoModel = Common.Models.Video.Video;

namespace Domain.Handlers.Twitch
{
    public class ParseVideoAtTwitchCommandHandler : IRequestHandler<ParseVideoAtTwitchCommand, VideoModel>
    {
        private readonly ApplicationDbContext _context;
        private readonly ITwitchService _twitchService;
        private readonly ILogger<ParseVideoAtTwitchCommandHandler> _logger;
        private const string baseUrl = "https://www.twitch.tv";

        public ParseVideoAtTwitchCommandHandler(ApplicationDbContext context, ITwitchService twitchService,
            ILogger<ParseVideoAtTwitchCommandHandler> logger)
        {
            _context = context;
            _twitchService = twitchService;
            _logger = logger;
        }

        public async Task<VideoModel> Handle(ParseVideoAtTwitchCommand request, CancellationToken cancellationToken)
        {
            var response = await _twitchService.GetVideoAsync(request.VideoId);
            if (response.data.IsNullOrEmpty())
            {
                return new VideoModel();
            }

            var twitchVideo = response.data.First();

            var bloger = await FindeChanel(twitchVideo.user_id);

            var video = new VideoModel
            {
                PlatformVideoId = (int) VideoPlatforms.Twitch,
                Name = twitchVideo.title,
                Description = twitchVideo.description,
                BlogerId = bloger?.Id ?? await AddBloger(twitchVideo.user_id),
                ChanelId = twitchVideo.user_id,
                //full example https://www.twitch.tv/videos/918656677?t=1h50m51s
                Url = $"{baseUrl}/videos/{twitchVideo.id}",
                VideoId = twitchVideo.id,
                ThumbnailUrl = twitchVideo.thumbnail_url.Replace("%{width}","480").Replace("%{height}","360"),
                CreateDateTime = twitchVideo.published_at,
                StartVideoSeconds = request.StartTime,
                EndVideoSeconds = request.EndTime,
                LikeCount = 0,
                DislikeCount = 0,
                CommentCount = 0,
                ViewCount = twitchVideo.view_count,
                ThemesId = request.ThemesId.ToArray(),
                Active = true,
            };

            await _context.Videos.AddAsync(video, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogDebug($"add video Id: {video.Id}");

            return video;
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
            var response = await _twitchService.GetChanelAsync(chanelId);

            if (response?.data.IsNullOrEmpty() ?? true)
                throw new Exception($"Не найден пользователь {chanelId}");

            var channel = response.data.First();
            var bloger = new BlogerModel
            {
                ChanelId = channel.id,
                Name = channel.login,
                NikcName = channel.display_name,
                Url = $"{baseUrl}/{channel.login}",
                Avatar = null,
                UrlAvatar = channel.profile_image_url,
                Active = true
            };

            await _context.Blogers.AddAsync(bloger);
            await _context.SaveChangesAsync();

            _logger.LogDebug($"add video Id: {bloger.Id}");

            return bloger.Id;
        }
    }
}
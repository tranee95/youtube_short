using Common.ViewModels;
using Domain.Commands.Video;
using MediatR;
using Microsoft.Extensions.Logging;
using Service.Video;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.Video
{
	public class GetVideoListCommandHandler : IRequestHandler<GetVideoListCommand, List<VideoListViewModel>>
	{
		private readonly IVideoService _videoService;
		private readonly ILogger<GetVideoListCommandHandler> _logger;

		public GetVideoListCommandHandler(IVideoService videoService, ILogger<GetVideoListCommandHandler> logger)
		{
			_videoService = videoService;
			_logger = logger;
		}

		public async Task<List<VideoListViewModel>> Handle(GetVideoListCommand request, CancellationToken cancellationToken)
		{
			try
			{
				_logger.LogInformation("load video list");
				return await _videoService.GetVidosAtFilters(request.Filter);
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				_logger.LogTrace(e.Message);
				return null;
			}
		}
	}
}

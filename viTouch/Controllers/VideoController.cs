using Common.Models.Video;
using Common.ViewModels;
using Domain.Commands.Video;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoModel = Common.Models.Video.Video;

namespace viTouch.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class VideoController : BaseApiController
	{
		[HttpGet]
		[ResponseCache(Location = ResponseCacheLocation.Any, Duration = ControllerConstants.CacheLifetime)]
		public async Task<Video> GetVideo([FromQuery] GetVideoCommand command) => await Mediator.Send(command);

		[HttpGet]
		[ResponseCache(Location = ResponseCacheLocation.Any, Duration = ControllerConstants.CacheLifetime)]
		public async Task<string> GetVideoImageById([FromQuery] GetVideoImageByIdCommand command) => await Mediator.Send(command);

		[HttpGet]
		[Authorize]
		[ResponseCache(Location = ResponseCacheLocation.Any, Duration = ControllerConstants.CacheLifetime)]
		public async Task<List<Video>> GetUserLikeVideo([FromQuery] GetUserLikeVideoCommand command) =>
			await Mediator.Send(command);

		[HttpGet]
		[Authorize]
		[ResponseCache(Location = ResponseCacheLocation.Any, Duration = ControllerConstants.CacheLifetime)]
		public async Task<List<Video>> GetUserDislikeVideo([FromQuery] GetUserDislikeVideoCommand command) =>
			await Mediator.Send(command);

		[HttpPost]
		public async Task<List<VideoListViewModel>> GetVideoList([FromBody] GetVideoListCommand command) =>
			await Mediator.Send(command);

		[HttpPost]
		public async Task<bool> AddLikeVideo([FromBody] AddLikeVideoCommand command) =>
			await Mediator.Send(command);

		[HttpPost]
		public async Task<bool> RemoveLikeVideo([FromBody] RemoveLikeVideoCommand command) =>
			await Mediator.Send(command);

		[HttpPost]
		public async Task<bool> AddDislikeVideo([FromBody] AddDislikeCommand command) =>
			await Mediator.Send(command);

		[HttpPost]
		public async Task<bool> RemoveDislikeVideo([FromBody] RemoveDislikeVideoCommand command) =>
			await Mediator.Send(command);

		[HttpPost]
		public async Task<List<Video>> GetVideosRange([FromBody] GetVideosRangeCommand command) =>
			await Mediator.Send(command);

		[HttpPost]
		public async Task<List<VideoListViewModel>> SeachVideoList([FromBody] SeachVideoListCommand command) =>
			await Mediator.Send(command);

		[HttpPost]
		public async Task<List<VideoModel>> GetDefaultVideos([FromBody] GetDefaultVideosCommand command) =>
			await Mediator.Send(command);

		// TODO: Check Admin Role
		[HttpPost]
		[Authorize]
		public async Task<bool> AddVideo([FromBody] AddVideoCommand command) => await Mediator.Send(command);

		[HttpDelete]
		[Authorize]
		public async Task<bool> DeleteVideo([FromBody] DeleteVidioCommand command) => await Mediator.Send(command);
	}
}

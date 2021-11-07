using Common.Models.Themes;
using Common.Models.User;
using Common.Models.Video;
using Common.ViewModels;
using Domain.Commands.Account;
using Domain.Commands.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace viTouch.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class UserController : BaseApiController
	{
		[HttpGet]
		[Authorize]
		[ResponseCache(Location = ResponseCacheLocation.Any, Duration = ControllerConstants.CacheLifetime)]
		public async Task<UserDataViewModel> GetUser([FromQuery] GetUserCommand command) =>
			await Mediator.Send(command);

		[HttpGet]
		[Authorize]
		[ResponseCache(Location = ResponseCacheLocation.Any, Duration = ControllerConstants.CacheLifetime)]
		public async Task<byte[]> GetUserAvatar([FromQuery] GetUserAvatarCommand command) =>
			await Mediator.Send(command);

		[HttpGet]
		[Authorize]
		[ResponseCache(Location = ResponseCacheLocation.Any, Duration = ControllerConstants.CacheLifetime)]
		public async Task<List<Themes>> GetUserThemes([FromQuery] GetUserThemesCommand command) =>
			await Mediator.Send(command);

		[HttpGet]
		[Authorize]
		[ResponseCache(Location = ResponseCacheLocation.Any, Duration = ControllerConstants.CacheLifetime)]
		public async Task<List<VideoListViewModel>> GetVideoUserSubscribeBlogers([FromQuery] GetVideoUserSubscribeBlogersCommand command) =>
			await Mediator.Send(command);

		[HttpGet]
		[Authorize]
		[ResponseCache(Location = ResponseCacheLocation.Any, Duration = ControllerConstants.CacheLifetime)]
		public async Task<List<VideoListViewModel>> GetVideoUserThemes([FromQuery] GetVideoUserThemesCommand command) =>
			await Mediator.Send(command);

		[HttpGet]
		[Authorize]
		[ResponseCache(Location = ResponseCacheLocation.Any, Duration = ControllerConstants.CacheLifetime)]
		public async Task<List<Video>> GetUserVideos([FromQuery] GetUserVideosCommand command) =>
			await Mediator.Send(command);

		[HttpGet]
		[Authorize]
		[ResponseCache(Location = ResponseCacheLocation.Any, Duration = ControllerConstants.CacheLifetime)]
		public async Task<List<VideoListViewModel>> GetUserVideoList([FromQuery] GetUserVideoListCommand command) =>
			await Mediator.Send(command);

		[HttpPost]
		[Authorize]
		public async Task<FilterVideoModel> AddUserFilterVideo([FromBody] AddUserFilterVideoCommand command) =>
			await Mediator.Send(command);

		[HttpPost]
		[Authorize]
		public async Task<bool> UserSubscrabeToBolger([FromBody] UserSubscribeToBolgerCommand command) =>
			await Mediator.Send(command);

		[HttpPost]
		[Authorize]
		public async Task<bool> AddUserThemes([FromBody] AddUserThemesCommand command) =>
			await Mediator.Send(command);

		[HttpPost]
		[Authorize]
		public async Task<bool> ToggleUserThemes([FromBody] ToggleUserThemesCommand command) =>
			await Mediator.Send(command);

		[HttpPost]
		[Authorize]
		public async Task<bool> ToggleUserBlogers([FromBody] ToggleUserBlogersCommand command) =>
			await Mediator.Send(command);

		[HttpDelete]
		[Authorize]
		public async Task<bool> DeleteUserThemes([FromBody] DeleteUsersThemesCommand command) =>
			await Mediator.Send(command);

		[HttpDelete]
		[Authorize]
		public async Task<bool> DeteleUserSubscrabeToBolger([FromBody] DeteleUserSubscrabeToBolgerCommand command) =>
			await Mediator.Send(command);

		[HttpPut]
		[Authorize]
		public async Task<bool> UpdateAvatar(int userId)
		{
			var avatar = Request.Form.Files[0];
			var memory = new MemoryStream();

			await avatar.OpenReadStream().CopyToAsync(memory);

			var result = await Mediator.Send(new UpdateAvataCommand
			{
				UserId = userId,
				Avatar = memory.ToArray()
			});
			return result;
		}

		[HttpPut]
		[Authorize]
		public async Task<bool> UpdateUserProfile([FromBody] UpdateUserProfileCommand command)
		{
			var result = await Mediator.Send(command);
			return result;
		}

		[HttpPut]
		[Authorize]
		public async Task<int> AddUserPoints([FromBody] AddUserPointsCommand command) => await Mediator.Send(command);
	}
}

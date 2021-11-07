using Common.ViewModels;
using DataContext;
using Domain.Commands.Account;
using Domain.Commands.Bloger;
using Domain.Commands.Favorite;
using Domain.Commands.User;
using Domain.Commands.Video;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace viTouch.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class AccountController : BaseApiController
	{
		[HttpGet]
		[ResponseCache(Location = ResponseCacheLocation.Any, Duration = ControllerConstants.CacheLifetime)]
		public async Task<bool> HasGoogleUser([FromQuery] HasGoogleUserCommand command) => await Mediator.Send(command);

		[HttpGet]
		[ResponseCache(Location = ResponseCacheLocation.Any, Duration = ControllerConstants.CacheLifetime)]
		public async Task<int> GetUserId([FromQuery] GetUserIdCommand command) => await Mediator.Send(command);

		[HttpPost]
		public async Task<ResultAuthenticationViewModel> Authentication([FromBody] AuthenticationCommand command)
		{
			var result = new ResultAuthenticationViewModel
			{
				UserToken = await Mediator.Send(command)
			};

			if (!string.IsNullOrEmpty(result.UserToken.Token))
			{
				result.Blogers = await Mediator.Send(new GetUserBlogersCommand { Count = 10, UserId = result.UserToken.UserId });
				result.Themes = await Mediator.Send(new GetUserThemesCommand { UserId = result.UserToken.UserId });
				result.Favorite = await Mediator.Send(new GetUserFavoriteCommand { UserId = result.UserToken.UserId });
				result.Like = await Mediator.Send(new GetUserLikeVideoCommand { UserId = result.UserToken.UserId });
				result.Dislike = await Mediator.Send(new GetUserDislikeVideoCommand { UserId = result.UserToken.UserId });

				var user = await Mediator.Send(new GetUserCommand { UserId = result.UserToken.UserId });
				result.UserDisplay = user.DisplayName;
				result.UserPoints = user.Points;
				result.AvatarUrl = user.AvatarUrl;
			}

			return result;
		}

		[HttpPost]
		public async Task<ResultAuthenticationViewModel> GoogeleAuthentication([FromBody] GoogeleAuthenticationCommand command)
		{
			var result = new ResultAuthenticationViewModel
			{
				UserToken = await Mediator.Send(command)
			};

			if (!string.IsNullOrEmpty(result.UserToken.Token))
			{
				result.Blogers = await Mediator.Send(new GetUserBlogersCommand { Count = 10, UserId = result.UserToken.UserId });
				result.Themes = await Mediator.Send(new GetUserThemesCommand { UserId = result.UserToken.UserId });
				result.Favorite = await Mediator.Send(new GetUserFavoriteCommand { UserId = result.UserToken.UserId });
				result.Like = await Mediator.Send(new GetUserLikeVideoCommand { UserId = result.UserToken.UserId });
				result.Dislike = await Mediator.Send(new GetUserDislikeVideoCommand { UserId = result.UserToken.UserId });

				var user = await Mediator.Send(new GetUserCommand { UserId = result.UserToken.UserId });
				result.UserDisplay = user.DisplayName;
				result.UserPoints = user.Points;
				result.AvatarUrl = user.AvatarUrl;
			}

			return result;
		}

		[HttpPost]
		public async Task<bool> BindingUserToGoogle([FromBody] BindingUserToGoogleCommand command) => await Mediator.Send(command);

		[HttpPost]
		public async Task<bool> SignUp([FromBody] SignUpCommand command)
		{
			var result = await Mediator.Send(command);
			return result;
		}

		[HttpPost]
		public async Task<bool> ForgetPassword([FromBody] ForgetPasswordCommand command)
		{
			var result = await Mediator.Send(command);
			return result;
		}
	}
}
using Common.ViewModels;
using Domain.Commands.Favorite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using FavoriteModel = Common.Models.Favorite.Favorite;

namespace viTouch.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class FavoriteController : BaseApiController
	{
		[HttpGet]
		[Authorize]
		[ResponseCache(Location = ResponseCacheLocation.Any, Duration = ControllerConstants.CacheLifetime)]
		public async Task<FavoriteViewModel> GetFavorite([FromQuery] GetFavoriteCommand command) =>
			await Mediator.Send(command);

		[HttpGet]
		[ResponseCache(Location = ResponseCacheLocation.Any, Duration = ControllerConstants.CacheLifetime)]
		public async Task<FavoriteModel> GetUserFavoriteByName([FromQuery] GetUserFavoriteByNameCommand command) =>
			await Mediator.Send(command);

		[HttpGet]
		[Authorize]
		[ResponseCache(Location = ResponseCacheLocation.Any, Duration = ControllerConstants.CacheLifetime)]
		public async Task<List<FavoriteViewModel>> GetUserFavorite([FromQuery] GetUserFavoriteCommand command) =>
			await Mediator.Send(command);

		[HttpGet]
		[Authorize]
		[ResponseCache(Location = ResponseCacheLocation.Any, Duration = ControllerConstants.CacheLifetime)]
		public async Task<FavoriteViewModel> GetUserDefaultFavorite([FromQuery] GetUserDefaultFavoriteCommand command) =>
			await Mediator.Send(command);

		[HttpPost]
		[Authorize]
		public async Task<FavoriteModel> CreateFavorite([FromBody] CreateFavoriteCommand command) =>
			await Mediator.Send(command);

		[HttpPost]
		[Authorize]
		public async Task<FavoriteModel> CreateDefaultFavorite([FromBody] CreateDefaultFavoriteCommand command) =>
			await Mediator.Send(command);

		[HttpPut]
		[Authorize]
		public async Task<bool> AddVideoToFavorite([FromBody] AddVideoToFavoriteCommand command) =>
			await Mediator.Send(command);

		[HttpPut]
		[Authorize]
		public async Task<bool> ToggleFavoriteLock([FromBody] ToggleFavoriteLockCommand command) =>
			await Mediator.Send(command);
		
		[HttpPut]
		public async Task<FavoriteModel> Update([FromBody] UpdateFavoriteCommand command) =>
			await Mediator.Send(command);

		[HttpPut]
		[Authorize]
		public async Task<bool> AddVideoToDefaultFavorite([FromBody] AddVideoToDefaultFavoriteCommand command) =>
			await Mediator.Send(command);

		[HttpDelete]
		[Authorize]
		public async Task<bool> RemoveFavorite([FromBody] RemoveFavoriteCommand command) =>
			await Mediator.Send(command);

		[HttpDelete]
		[Authorize]
		public async Task<bool> RemoveDefaultFavorite([FromBody] RemoveDefaultFavoriteCommand command) =>
			await Mediator.Send(command);

		[HttpDelete]
		[Authorize]
		public async Task<bool> RemoveVideoAtFavorite([FromBody] RemoveVideoAtFavoriteCommmad commmad) =>
			await Mediator.Send(commmad);

		[HttpDelete]
		[Authorize]
		public async Task<bool> RemoveVideoAtDefaultFavorite([FromBody] RemoveVideoAtDefaultFavoriteCommand command) =>
			await Mediator.Send(command);
	}
}

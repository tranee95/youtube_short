using Common.Models.Bloger;
using Domain.Commands.Bloger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace viTouch.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class BlogerController : BaseApiController
	{
		[HttpGet]
		[Authorize]
		[ResponseCache(Location = ResponseCacheLocation.Any, Duration = ControllerConstants.CacheLifetime)]
		public async Task<IList<Bloger>> GetUserBlogers([FromQuery] GetUserBlogersCommand command) => await Mediator.Send(command);

		[HttpPost]
		public async Task<List<Bloger>> GetFilterBlogers([FromBody] GetFilterBlogersCommand command) => await Mediator.Send(command);

		[HttpGet]
		[ResponseCache(Location = ResponseCacheLocation.Any, Duration = ControllerConstants.CacheLifetime)]
		public async Task<Bloger> GetBloger([FromQuery] GetBlogerCommand command) => await Mediator.Send(command);

		[HttpPost]
		[Authorize]
		public async Task<bool> CreateBloger([FromBody] SetCreateBlogerCommand command) => await Mediator.Send(command);

		[HttpDelete]
		[Authorize]
		public async Task<bool> DeleteBloger([FromBody] DeleteBlogerCommand command) => await Mediator.Send(command);

		[HttpPut]
		[Authorize]
		public async Task<Bloger> UpdateBloger([FromBody] UpdateBlogerCommand command) => await Mediator.Send(command);
	}
}
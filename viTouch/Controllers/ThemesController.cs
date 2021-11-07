using Common.Models.Themes;
using Domain.Commands.Themes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace viTouch.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class ThemesController : BaseApiController
	{
		[HttpGet]
		[ResponseCache(Location = ResponseCacheLocation.Any, Duration = ControllerConstants.CacheLifetime)]
		public async Task<List<Themes>> GetAllThemes() => await Mediator.Send(new GetAllThemesComamand());
		
		[HttpPost]
		public async Task<List<Themes>> GetFilterThemes([FromBody] GetFilterThemesCommand command) =>
			await Mediator.Send(command);

		// TODO: Check Admin Role

		[HttpPost]
		[Authorize]
		public async Task<Themes> AddTheme([FromBody] AddThemeCommand command) => await Mediator.Send(command);

		[HttpDelete]
		[Authorize]
		public async Task<bool> DeleteTheme([FromQuery] DeleteThemeCommand command) => await Mediator.Send(command);

		[HttpPost]
		[Authorize]
		public async Task<Tags> AddTag([FromBody] AddTagsCommand command) => await Mediator.Send(command);

		[HttpDelete]
		[Authorize]
		public async Task<bool> DeleteTag([FromBody] DeleteTagCommand command) => await Mediator.Send(command);
	}
}

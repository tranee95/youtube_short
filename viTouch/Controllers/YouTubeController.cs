using Common.Models.Video;
using Domain.Commands.YouTube;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace viTouch.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class YouTubeController : BaseApiController
	{
		[HttpPost]
		public async Task<Video> ParseVideoAtYouTube([FromBody] ParseVideoAtYouTubeCommand command) =>
			await Mediator.Send(command);

	}
}

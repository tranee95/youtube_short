using Common.Models.Video;
using Domain.Commands.Twitch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace viTouch.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TwitchController : BaseApiController
    {
        [HttpPost]
        public async Task<Video> ParseVideo([FromBody] ParseVideoAtTwitchCommand command) =>
            await Mediator.Send(command);
    }
}
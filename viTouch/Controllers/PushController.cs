using Common.Models.Push;
using Domain.Commands.Push;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace viTouch.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class PushController : BaseApiController
	{
		// TODO: добавить проверку на админа
		[HttpPost]
		public async Task<PushResult> SentTransactional([FromBody] SentTransactionalCommand command) => await Mediator.Send(command);
	}
}

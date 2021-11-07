using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace viTouch.Controllers
{
	public class BaseApiController : ControllerBase
	{
		private IMediator _mediator;

		protected IMediator Mediator
		{
			get
			{
				_mediator ??= _mediator = HttpContext.RequestServices.GetService<IMediator>();
				return _mediator;
			}
		}
	}
}
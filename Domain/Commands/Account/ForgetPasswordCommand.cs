using MediatR;

namespace Domain.Commands.Account
{
	public class ForgetPasswordCommand : IRequest<bool>
	{
		public string Email { get; set; }
	}
}

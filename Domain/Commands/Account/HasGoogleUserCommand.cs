using MediatR;

namespace Domain.Commands.Account
{
	public class HasGoogleUserCommand : IRequest<bool>
	{
		public string GoogleUserId { get; set; }
	}
}

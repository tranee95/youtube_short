using MediatR;

namespace Domain.Commands.Account
{
	public class GetUserIdCommand : IRequest<int>
	{
		public string Email { get; set; }
	}
}

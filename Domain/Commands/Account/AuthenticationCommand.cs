using Common.Models.User;
using MediatR;

namespace Domain.Commands.Account
{
	public class AuthenticationCommand : IRequest<UserToken>
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
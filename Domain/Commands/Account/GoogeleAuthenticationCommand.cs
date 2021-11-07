using Common.Models.Google;
using Common.Models.User;
using MediatR;

namespace Domain.Commands.Account
{
	public class GoogeleAuthenticationCommand : GoogleUser, IRequest<UserToken>
	{
	}
}

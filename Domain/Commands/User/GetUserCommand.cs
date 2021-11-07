using Common.ViewModels;
using MediatR;

namespace Domain.Commands.User
{
	public class GetUserCommand : IRequest<UserDataViewModel>
	{
		public int UserId { get; set; }
	}
}

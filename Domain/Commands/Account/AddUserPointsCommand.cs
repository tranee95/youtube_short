using MediatR;

namespace Domain.Commands.Account
{
	public class AddUserPointsCommand : IRequest<int>
	{
		public int UserId { get; set; }
		public int Points { get; set; }
	}
}

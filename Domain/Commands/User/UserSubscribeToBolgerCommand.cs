using System.Collections.Generic;
using MediatR;

namespace Domain.Commands.User
{
	public class UserSubscribeToBolgerCommand : IRequest<bool>
	{
		public int UserId { get; set; }
		public List<int> BlogersId { get; set; }
	}
}

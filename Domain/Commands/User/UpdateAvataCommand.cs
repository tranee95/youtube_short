using MediatR;

namespace Domain.Commands.User
{
	public class UpdateAvataCommand: IRequest<bool>
	{
		public int UserId { get; set; }
		public byte[] Avatar { get; set; }
	}
}

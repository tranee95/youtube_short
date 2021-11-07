using MediatR;

namespace Domain.Commands.User
{
	public class GetUserAvatarCommand: IRequest<byte[]>
	{
		public int UserId { get; set; }
	}
}

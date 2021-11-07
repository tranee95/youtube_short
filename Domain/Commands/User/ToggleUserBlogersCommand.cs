using MediatR;

namespace Domain.Commands.User
{
	public class ToggleUserBlogersCommand: IRequest<bool>
	{
		public int UserId { get; set; }
		public int BlogerId { get; set; }
	}
}

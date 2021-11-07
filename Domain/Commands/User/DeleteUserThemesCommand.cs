using MediatR;

namespace Domain.Commands.User
{
	public class DeleteUsersThemesCommand: IRequest<bool>
	{
		public int UserId { get; set; }
		public int ThemeId { get; set; }
	}
}

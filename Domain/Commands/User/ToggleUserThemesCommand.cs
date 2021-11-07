using MediatR;

namespace Domain.Commands.User
{
	public class ToggleUserThemesCommand : IRequest<bool>
	{
		public int UserId { get; set; }
		public int ThemesId { get; set; }
	}
}

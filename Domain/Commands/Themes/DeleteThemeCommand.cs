using MediatR;

namespace Domain.Commands.Themes
{
	public class DeleteThemeCommand : IRequest<bool>
	{
		public int ThemeId { get; set; }
	}
}
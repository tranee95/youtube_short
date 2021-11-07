using MediatR;

namespace Domain.Commands.Themes
{
	public class AddThemeCommand : IRequest<Common.Models.Themes.Themes>
	{
		public string Name { get; set; }
	}
}
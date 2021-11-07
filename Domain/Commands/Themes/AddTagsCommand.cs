using Common.Models.Themes;
using MediatR;

namespace Domain.Commands.Themes
{
	public class AddTagsCommand : IRequest<Tags>
	{
		public string Name { get; set; }
		public int ThemesId { get; set; }
	}
}

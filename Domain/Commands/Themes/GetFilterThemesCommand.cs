using MediatR;
using System.Collections.Generic;
using ThemesModle = Common.Models.Themes.Themes;

namespace Domain.Commands.Themes
{
	public class GetFilterThemesCommand : IRequest<List<ThemesModle>>
	{
		public string ThemesName { get; set; }
	}
}

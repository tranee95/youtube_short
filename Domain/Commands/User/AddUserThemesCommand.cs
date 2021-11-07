using MediatR;
using System.Collections.Generic;
using ThemesModel = Common.Models.Themes.Themes;

namespace Domain.Commands.User
{
	public class AddUserThemesCommand : IRequest<bool>
	{
		public int UserId { get; set; }
		public List<ThemesModel> Themes { get; set; }
	}
}

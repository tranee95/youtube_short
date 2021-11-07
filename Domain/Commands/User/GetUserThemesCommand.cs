using MediatR;
using System.Collections.Generic;

namespace Domain.Commands.User
{
	public class GetUserThemesCommand : IRequest<List<Common.Models.Themes.Themes>>
	{
		public int UserId { get; set; }
	}
}

using System.Collections.Generic;
using MediatR;

namespace Domain.Commands.Themes
{
	public class GetAllThemesComamand : IRequest<List<Common.Models.Themes.Themes>>
	{
	}
}
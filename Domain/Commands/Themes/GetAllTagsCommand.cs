using Common.Models.Themes;
using MediatR;
using System.Collections.Generic;

namespace Domain.Commands.Themes
{
	public class GetAllTagsCommand : IRequest<List<Tags>> { }
}

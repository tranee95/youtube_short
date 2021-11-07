using Common.Models.User;
using MediatR;
using System.Collections.Generic;

namespace Domain.Commands.User
{
	public class AddUserFilterVideoCommand : IRequest<FilterVideoModel>
	{
		public int UserId { get; set; }
		public List<int> BlogerId { get; set; }
		public List<int> ThemesId { get; set; }
	}
}

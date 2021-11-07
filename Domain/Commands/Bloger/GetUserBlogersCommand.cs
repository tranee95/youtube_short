using System.Collections.Generic;
using MediatR;
using BlogerModel = Common.Models.Bloger.Bloger;

namespace Domain.Commands.Bloger
{
	public class GetUserBlogersCommand: IRequest<List<BlogerModel>>
	{
		public int UserId { get; set; }
		public int Count { get; set; }
	}
}

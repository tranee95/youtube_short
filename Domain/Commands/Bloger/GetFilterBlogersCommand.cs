using MediatR;
using System.Collections.Generic;
using BlogerModel = Common.Models.Bloger.Bloger;

namespace Domain.Commands.Bloger
{
	public class GetFilterBlogersCommand : IRequest<List<BlogerModel>>
	{
		public string BlogerNickName { get; set; }
		public string BlogerName { get; set; }
		public int Count { get; set; }
	}
}

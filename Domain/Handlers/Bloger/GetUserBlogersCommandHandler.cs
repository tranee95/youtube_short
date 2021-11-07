using DataContext;
using Domain.Commands.Bloger;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlogerModel = Common.Models.Bloger.Bloger;

namespace Domain.Handlers.Bloger
{
	public class GetUserBlogersCommandHandler : IRequestHandler<GetUserBlogersCommand, List<BlogerModel>>
	{
		public readonly ApplicationDbContext _context;

		public GetUserBlogersCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<BlogerModel>> Handle(GetUserBlogersCommand request, CancellationToken cancellationToken)
		{
			var result =
				await _context.UserBloger
					.Where(s => s.UserId == request.UserId)
					.Join(_context.Blogers,
						user => user.BlogerId,
						bloger => bloger.Id,
						(user, bloger) => new BlogerModel
						{
							Id = bloger.Id,
							Name = bloger.Name,
							NikcName = bloger.NikcName,
							Url = bloger.Url,
							Subscribers = bloger.Subscribers,
							Avatar = bloger.Avatar,
							Active = bloger.Active

						})
						.ToListAsync(cancellationToken: cancellationToken);

			return result;
		}
	}
}

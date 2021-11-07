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
	public class GetFilterBlogersCommandHandler : IRequestHandler<GetFilterBlogersCommand, List<BlogerModel>>
	{
		private readonly ApplicationDbContext _context;

		public GetFilterBlogersCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<BlogerModel>> Handle(GetFilterBlogersCommand request, CancellationToken cancellationToken)
		{
			var blogers =
					_context
						.Blogers
						.AsNoTracking()
						.Where(s => s.Active == true);

			if (!string.IsNullOrEmpty(request.BlogerName))
			{
				blogers = blogers.Where(s => EF.Functions.Like(s.Name.ToLower(), $"%{request.BlogerName.ToLower()}%"));
			}

			
			if (!string.IsNullOrEmpty(request.BlogerNickName))
			{
				blogers = blogers.Where(s => EF.Functions.Like(s.NikcName.ToLower(), $"%{request.BlogerNickName.ToLower()}%"));
			}

			blogers = blogers
						.Take(request.Count == 0 ? 10 : request.Count)
						.OrderBy(s => s.Id);

			return await blogers.ToListAsync(cancellationToken);
		}
	}
}

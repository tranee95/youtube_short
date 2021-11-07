using System.Threading;
using System.Threading.Tasks;
using DataContext;
using Domain.Commands.Bloger;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BlogerModel = Common.Models.Bloger.Bloger;

namespace Domain.Handlers.Bloger
{
	public class GetBlogerCommandHandler : IRequestHandler<GetBlogerCommand, BlogerModel>
	{
		private readonly ApplicationDbContext _context;

		public GetBlogerCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<BlogerModel> Handle(GetBlogerCommand request, CancellationToken cancellationToken)
		{
			return await _context
			             .Blogers
			             .AsNoTracking()
			             .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
		}
	}
}
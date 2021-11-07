using System.Threading;
using System.Threading.Tasks;
using DataContext;
using Domain.Commands.Bloger;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BlogerModel = Common.Models.Bloger.Bloger;

namespace Domain.Handlers.Bloger
{
	public class UpdateBlogerCommandHandler : IRequestHandler<UpdateBlogerCommand, BlogerModel>
	{
		private readonly ApplicationDbContext _context;

		public UpdateBlogerCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<BlogerModel> Handle(UpdateBlogerCommand request, CancellationToken cancellationToken)
		{
			var bloger = await _context
			                   .Blogers
			                   .AsNoTracking()
			                   .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

			if (bloger != null)
			{
				bloger.Name = request.Name;
				bloger.Active = request.Active;
				bloger.NikcName = request.NikcName;
				bloger.Url = request.Url;
				bloger.UrlAvatar = request.UrlAvatar;

				await _context.SaveChangesAsync(cancellationToken);
			}

			return bloger;
		}
	}
}
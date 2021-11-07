using System.Threading;
using System.Threading.Tasks;
using DataContext;
using Domain.Commands.Bloger;
using MediatR;
using BlogerModel = Common.Models.Bloger.Bloger;

namespace Domain.Handlers.Bloger
{
	public class SetCreateBlogerCommandHandler : IRequestHandler<SetCreateBlogerCommand, bool>
	{
		private readonly ApplicationDbContext _context;

		public SetCreateBlogerCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(SetCreateBlogerCommand request, CancellationToken cancellationToken)
		{
			var result = new BlogerModel
			             {
				             Name = request.Name,
							 NikcName = request.NikcName,
							 Subscribers = request.Subscribers,
							 Url = request.Url,
							 UrlAvatar = request.UrlAvatar,
							 Active = true,
			             };

			await _context.Blogers.AddAsync(result, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}
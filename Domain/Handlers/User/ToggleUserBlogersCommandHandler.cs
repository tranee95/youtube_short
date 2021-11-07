using Common.Models.User;
using DataContext;
using Domain.Commands.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.User
{
	public class ToggleUserBlogersCommandHandler : IRequestHandler<ToggleUserBlogersCommand, bool>
	{
		private readonly ApplicationDbContext _context;

		public ToggleUserBlogersCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(ToggleUserBlogersCommand request, CancellationToken cancellationToken)
		{
			var bloger =
				await _context
						.UserBloger
						.AsNoTracking()
						.FirstOrDefaultAsync(s => s.UserId.Equals(request.UserId) && s.BlogerId.Equals(request.BlogerId),
							cancellationToken);

			if (bloger is null)
			{
				await _context
						.UserBloger
						.AddAsync(new UserBloger
						{
							UserId = request.UserId,
							BlogerId = request.BlogerId,
							Active = true
						}, cancellationToken);
			}

			if (bloger != null)
			{
				_context.UserBloger.Remove(bloger);
			}

			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
	}
}

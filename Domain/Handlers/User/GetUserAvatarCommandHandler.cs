using System.Threading;
using System.Threading.Tasks;
using DataContext;
using Domain.Commands.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Handlers.User
{
	public class GetUserAvatarCommandHandler: IRequestHandler<GetUserAvatarCommand, byte[]>
	{
		private readonly ApplicationDbContext _context;

		public GetUserAvatarCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<byte[]> Handle(GetUserAvatarCommand request, CancellationToken cancellationToken)
		{
			var user = 
				await _context
					.Users
					.AsNoTracking()
					.FirstOrDefaultAsync(s => s.Id == request.UserId, cancellationToken: cancellationToken);

			return user.Avatar;
		}
	}
}

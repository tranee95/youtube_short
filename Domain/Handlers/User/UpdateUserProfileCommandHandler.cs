using System.Threading;
using System.Threading.Tasks;
using DataContext;
using Domain.Commands.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Security;

namespace Domain.Handlers.User
{
	public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, bool>
	{
		private readonly ApplicationDbContext _context;
		private readonly IMd5Hash _md5Hash;

		public UpdateUserProfileCommandHandler(ApplicationDbContext context, IMd5Hash md5Hash)
		{
			_context = context;
			_md5Hash = md5Hash;
		}

		public async Task<bool> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
		{
			var user = await _context.Users.FirstOrDefaultAsync(s => s.Id == request.UserId, cancellationToken);

			if (user is null) return false;

			if (!string.IsNullOrEmpty(request.Email))
			{
				user.Email = request.Email;
				user.DisplayName = request.Email;

				await _context.SaveChangesAsync(cancellationToken);
			}

			if (!string.IsNullOrEmpty(request.Password) && request.Password == request.ConfirmPassword)
			{
				var hash = _md5Hash.GetMd5Hash(request.Password);
				user.PasswordHash = hash;

				await _context.SaveChangesAsync(cancellationToken);
			}

			return true;
		}
	}
}
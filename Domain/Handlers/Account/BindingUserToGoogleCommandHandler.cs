using DataContext;
using Domain.Commands.Account;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Domain.Handlers.Account
{
	public class BindingUserToGoogleCommandHandler : IRequestHandler<BindingUserToGoogleCommand, bool>
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<BindingUserToGoogleCommandHandler> _logger;

		public BindingUserToGoogleCommandHandler(ApplicationDbContext context, ILogger<BindingUserToGoogleCommandHandler> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task<bool> Handle(BindingUserToGoogleCommand request, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(request.GoogleUserId)) return false;

			var user =
				await _context
					.Users
					.FirstOrDefaultAsync(s => s.Id.Equals(request.UserId),
						cancellationToken);

			if (user is null) return false;

			user.GoogleId = request.GoogleUserId;
			user.ImageUrl = request.ImageUrl;

			await _context.SaveChangesAsync(cancellationToken);
			_logger.LogWarning($"Binding User:{user.Id} go Google:{request.GoogleUserId}");

			return true;
		}
	}
}

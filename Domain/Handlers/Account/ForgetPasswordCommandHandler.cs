using DataContext;
using Domain.Commands.Account;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.Account
{
	public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, bool>
	{
		private readonly ApplicationDbContext _context;
		private readonly IEmailService _emailService;

		public ForgetPasswordCommandHandler(ApplicationDbContext context, IEmailService emailService)
		{
			_context = context;
			_emailService = emailService;
		}

		public async Task<bool> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
		{
			var user =
				await _context
						.Users
						.AsNoTracking()
						.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

			if (user is null) return false;

			await _emailService.SendEmailAsync(user.Email, "ForgetPassword", "url");

			return true;
		}
	}
}
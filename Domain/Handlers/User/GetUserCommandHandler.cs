using Common.ViewModels;
using DataContext;
using Domain.Commands.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.User
{
	public class GetUserCommandHandler : IRequestHandler<GetUserCommand, UserDataViewModel>
	{
		private readonly ApplicationDbContext _context;

		public GetUserCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<UserDataViewModel> Handle(GetUserCommand request, CancellationToken cancellationToken)
		{
			return
				await _context
					.Users
					.AsNoTracking()
					.Select(s => new UserDataViewModel
					{
						UserId = s.Id,
						DisplayName = s.DisplayName,
						Email = s.Email,
						Points = s.Points,
						Active = s.Active,
						AvatarUrl = s.ImageUrl
					})
					.FirstOrDefaultAsync(s => s.UserId == request.UserId, cancellationToken: cancellationToken);

		}
	}
}

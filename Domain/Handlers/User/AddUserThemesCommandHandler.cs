using Domain.Commands.User;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Models.User;
using DataContext;

namespace Domain.Handlers.User
{
	public class AddUserThemesCommandHandler : IRequestHandler<AddUserThemesCommand, bool>
	{
		private readonly ApplicationDbContext _context;

		public AddUserThemesCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(AddUserThemesCommand request, CancellationToken cancellationToken)
		{
			var userThemes = _context.UserThemes.Where(s => s.UserId == request.UserId);

			_context.UserThemes.RemoveRange(userThemes);

			var result = 
				request
					.Themes
					.Select(item => new UserThemes
					{
						ThemeId = item.Id,
						UserId = request.UserId,
						Active = true
					}).ToList();

			await _context.UserThemes.AddRangeAsync(result, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}

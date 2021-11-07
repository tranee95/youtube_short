using Common.Models.User;
using DataContext;
using Domain.Commands.User;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers.User
{
	public class AddUserFilterVideoCommandHandler : IRequestHandler<AddUserFilterVideoCommand, FilterVideoModel>
	{
		private readonly ApplicationDbContext _context;

		public AddUserFilterVideoCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<FilterVideoModel> Handle(AddUserFilterVideoCommand request, CancellationToken cancellationToken)
		{
			var userFilter = new FilterVideoModel
			{
				UserId = request.UserId,
				BlogersId = request.BlogerId,
				ThemesId = request.ThemesId,
				Active = true
			};

			await _context.UserFilterVideos.AddAsync(userFilter, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);

			return userFilter;
		}
	}
}

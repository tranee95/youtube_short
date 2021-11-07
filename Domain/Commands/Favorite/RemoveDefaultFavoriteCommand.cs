using MediatR;

namespace Domain.Commands.Favorite
{
	public class RemoveDefaultFavoriteCommand : IRequest<bool>
	{
		public int UserId { get; set; }
	}
}

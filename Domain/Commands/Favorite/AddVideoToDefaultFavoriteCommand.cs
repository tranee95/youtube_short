using MediatR;

namespace Domain.Commands.Favorite
{
	public class AddVideoToDefaultFavoriteCommand : IRequest<bool>
	{
		public int UserId { get; set; }
		public int VideoId { get; set; }
	}
}

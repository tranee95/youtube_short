using MediatR;

namespace Domain.Commands.Favorite
{
	public class RemoveVideoAtDefaultFavoriteCommand : IRequest<bool>
	{
		public int UserId { get; set; }
		public int VideoId { get; set; }
	}
}

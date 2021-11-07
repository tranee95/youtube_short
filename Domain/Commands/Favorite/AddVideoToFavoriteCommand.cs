using MediatR;

namespace Domain.Commands.Favorite
{
	public class AddVideoToFavoriteCommand : IRequest<bool>
	{
		public int Id { get; set; }
		public string Guid { get; set; }
		public int VideoId { get; set; }
	}
}

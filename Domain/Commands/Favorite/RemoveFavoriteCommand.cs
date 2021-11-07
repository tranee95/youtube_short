using MediatR;

namespace Domain.Commands.Favorite
{
	public class RemoveFavoriteCommand: IRequest<bool>
	{
		public int Id { get; set; }
		public string Guid { get; set; }
	}
}

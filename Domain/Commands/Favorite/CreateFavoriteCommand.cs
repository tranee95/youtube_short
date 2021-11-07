using MediatR;
using FavoriteModel = Common.Models.Favorite.Favorite;

namespace Domain.Commands.Favorite
{
	public class CreateFavoriteCommand: IRequest<FavoriteModel>
	{
		public int UserId { get; set; }
		public string Guid { get; set; }
		public string Name { get; set; }
		public bool IsDefault { get; set; }
		public bool IsLock { get; set; }
	}
}

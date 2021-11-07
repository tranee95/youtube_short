using MediatR;
using FavoriteModel = Common.Models.Favorite.Favorite;

namespace Domain.Commands.Favorite
{
	public class GetUserFavoriteByNameCommand : IRequest<FavoriteModel>
	{
		public int UserId { get; set; }
		public string FavoriteName { get; set; }
	}
}

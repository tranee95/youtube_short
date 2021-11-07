using MediatR;
using FavoriteModel = Common.Models.Favorite.Favorite;

namespace Domain.Commands.Favorite
{
	public class CreateDefaultFavoriteCommand : IRequest<FavoriteModel>
	{
		public int UserId { get; set; }
		public int VideoId { get; set; }
	}
}

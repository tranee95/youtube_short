using Common.ViewModels;
using MediatR;
using FavoriteModel = Common.Models.Favorite.Favorite;

namespace Domain.Commands.Favorite
{
	public class GetFavoriteCommand : IRequest<FavoriteViewModel>
	{
		public int Id { get; set; }
		public string Guid { get; set; }
	}
}

using Common.ViewModels;
using MediatR;


namespace Domain.Commands.Favorite
{
	public class GetUserDefaultFavoriteCommand : IRequest<FavoriteViewModel>
	{
		public int Id { get; set; }
	}
}

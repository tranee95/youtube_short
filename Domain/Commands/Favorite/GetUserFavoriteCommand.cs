using MediatR;
using System.Collections.Generic;
using Common.ViewModels;

namespace Domain.Commands.Favorite
{
	public class GetUserFavoriteCommand : IRequest<List<FavoriteViewModel>>
	{
		public int UserId { get; set; }
	}
}

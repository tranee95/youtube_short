using MediatR;
using System.Collections.Generic;
using BlogerModel = Common.Models.Bloger.Bloger;
using FavoriteModel = Common.Models.Favorite.Favorite;
using ThemeModels = Common.Models.Themes.Themes;

namespace Domain.Commands.Account
{
	public class SignUpCommand : IRequest<bool>
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public List<ThemeModels> Themes { get; set; }
		public List<BlogerModel> Blogers { get; set; }
		public List<FavoriteModel> Favorite { get; set; }
		public List<int> Like { get; set; }
		public List<int> Dislike { get; set; }
	}
}
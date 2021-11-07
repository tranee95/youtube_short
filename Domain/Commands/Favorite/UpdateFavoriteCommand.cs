using System.ComponentModel.DataAnnotations;
using MediatR;
using FavoriteModel = Common.Models.Favorite.Favorite;

namespace Domain.Commands.Favorite
{
    public class UpdateFavoriteCommand : IRequest<FavoriteModel>
    {
        [Required]
        public string Guid { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}
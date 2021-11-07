using System;
using DataContext;
using Domain.Commands.Favorite;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FavoriteModel = Common.Models.Favorite.Favorite;

namespace Domain.Handlers.Favorite
{
    public class UpdateFavoriteCommandHandler : IRequestHandler<UpdateFavoriteCommand, FavoriteModel>
    {
        private readonly ApplicationDbContext _context;

        public UpdateFavoriteCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FavoriteModel> Handle(UpdateFavoriteCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentNullException(nameof(request));

            var favorite = await _context.Favorite.FirstAsync(x => x.Guid == request.Guid, cancellationToken: cancellationToken);

            favorite.Name = request.Name;
            await _context.SaveChangesAsync(cancellationToken);

            return favorite;
        }
    }
}
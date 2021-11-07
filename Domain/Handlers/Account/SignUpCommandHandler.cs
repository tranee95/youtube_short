using Common.Models.User;
using Common.Models.Video;
using DataContext;
using Domain.Commands.Account;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Security;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlogerModel = Common.Models.Bloger.Bloger;
using FavoriteModel = Common.Models.Favorite.Favorite;
using ThemesModel = Common.Models.Themes.Themes;

namespace Domain.Handlers.Account
{
	public class SignUpCommandHandler : IRequestHandler<SignUpCommand, bool>
	{
		private readonly ApplicationDbContext _context;
		private readonly IMd5Hash _md5Hash;

		public SignUpCommandHandler(ApplicationDbContext context, IMd5Hash md5Hash)
		{
			_context = context;
			_md5Hash = md5Hash;
		}

		public async Task<bool> Handle(SignUpCommand request, CancellationToken cancellationToken)
		{
			if (request.Password != request.ConfirmPassword)
				return false;

			if (_context.Users.AsNoTracking().Any(s => s.Email == request.Email))
				return false;

			using (var tr = await _context.Database.BeginTransactionAsync(cancellationToken))
			{
				var userData = CreateUserData(request);
				await _context.Users.AddAsync(userData, cancellationToken);
				await _context.SaveChangesAsync(cancellationToken);

				if (request.Themes != null)
				{
					await _context
						.UserThemes
						.AddRangeAsync(CreateUserThemes(request.Themes, userData.Id), cancellationToken);
				}

				if (request.Blogers != null)
				{
					await _context
						.UserBloger
						.AddRangeAsync(CreateUserBlogers(request.Blogers, userData.Id), cancellationToken);
				}

				if (request.Themes != null)
				{
					await _context
						.UserFilterVideos
						.AddRangeAsync(CreateUserFilterVideos(request.Themes, request.Blogers, userData.Id), cancellationToken);
				}

				if (request.Like != null)
				{
					await _context
						.Likes
						.AddRangeAsync(CreateUserLike(request.Like, userData.Id), cancellationToken);
				}

				if (request.Dislike != null)
				{
					await _context
						.Dislikes
						.AddRangeAsync(CreateUserDislike(request.Dislike, userData.Id), cancellationToken);
				}

				if (request.Favorite != null)
				{
					await _context
						.Favorite
						.AddRangeAsync(CreateFavorite(request.Favorite, userData.Id), cancellationToken);
				}

				await _context.SaveChangesAsync(cancellationToken);
				await tr.CommitAsync(cancellationToken);

				return true;
			}
		}

		private UserData CreateUserData(SignUpCommand data)
		{
			var hash = _md5Hash.GetMd5Hash(data.Password);

			var userData = new UserData
			{
				Email = data.Email.Contains("@") ? data.Email : string.Empty,
				PasswordHash = hash,
				Avatar = new byte[0],
				DisplayName = data.Email,
				Active = true,
				Points = 0,
				Role = "user"
			};

			return userData;
		}

		private IEnumerable<FilterVideoModel> CreateUserFilterVideos(IEnumerable<ThemesModel> themes,
			IEnumerable<BlogerModel> blogers, int userId)
		{
			blogers ??= new List<BlogerModel>();
			themes ??= new List<ThemesModel>();

			var result =
				blogers.Select(b => new FilterVideoModel
				{
					UserId = userId,
					BlogersId = new List<int> { b.Id },
					ThemesId = new List<int>(),
					Active = true,
				}).ToList();

			result.AddRange(themes
				.Select(t => new FilterVideoModel
				{
					UserId = userId,
					BlogersId = new List<int>(),
					ThemesId = new List<int> { t.Id },
					Active = true,
				}));

			return result;
		}

		private IEnumerable<FavoriteModel> CreateFavorite(List<FavoriteModel> favorites, int userId)
		{
			var result =
				favorites
					.Select(s => new FavoriteModel
					{
						UserId = userId,
						Guid = s.Guid,
						IsDefaultList = s.IsDefaultList,
						Name = s.Name,
						VideosId = s.VideosId,
						Active = true
					});

			return result;
		}

		private IEnumerable<Likes> CreateUserLike(List<int> videoLikes, int userId)
		{
			var result =
				videoLikes
					.Select(s => new Likes
					{
						VideoId = s,
						UserId = userId,
						Active = true
					});

			return result;
		}

		private IEnumerable<Dislikes> CreateUserDislike(List<int> videoDislike, int userId)
		{
			var result =
				videoDislike
					.Select(s => new Dislikes
					{
						VideoId = s,
						UserId = userId,
						Active = true
					});

			return result;
		}

		private IEnumerable<UserThemes> CreateUserThemes(IEnumerable<ThemesModel> themes, int userId)
		{
			if (themes is null) return new List<UserThemes>();

			var result =
				themes
					.Select(theme => new UserThemes
					{
						ThemeId = theme.Id,
						UserId = userId,
						Active = true
					});

			return result;
		}

		private IEnumerable<UserBloger> CreateUserBlogers(IEnumerable<BlogerModel> blogers, int userId)
		{
			if (blogers is null) return new List<UserBloger>();

			var result =
				blogers
					.Select(theme => new UserBloger
					{
						BlogerId = theme.Id,
						UserId = userId,
						Active = true
					});

			return result;
		}
	}
}
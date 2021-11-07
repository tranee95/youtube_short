using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Models.User;
using DataContext;
using Domain.Commands.Account;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Handlers.Account
{
	public class GoogeleAuthenticationCommandHandler: IRequestHandler<GoogeleAuthenticationCommand, UserToken>
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<GoogeleAuthenticationCommandHandler> _logger;

		public GoogeleAuthenticationCommandHandler(ApplicationDbContext context, ILogger<GoogeleAuthenticationCommandHandler> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task<UserToken> Handle(GoogeleAuthenticationCommand request, CancellationToken cancellationToken)
		{
			if (request.AccessToken is null) return new UserToken();
			var identity = await GetIdentity(request.UserId);

			if (identity is null) return new UserToken();

			var now = DateTime.UtcNow;

			var jwt = new JwtSecurityToken(
				AuthenticationOptions.ISSUER,
				AuthenticationOptions.AUDIENCE,
				notBefore: now,
				claims: identity.Claims,
				expires: now.Add(TimeSpan.FromMinutes(AuthenticationOptions.LIFETIME)),
				signingCredentials: new SigningCredentials(
					AuthenticationOptions.GetSymmetricSecurityKey(),
					SecurityAlgorithms.HmacSha256));

			var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

			return new UserToken
			{
				Token = encodedJwt,
				UserId = int.Parse(identity.Name)
			};
		}

		private async Task<ClaimsIdentity> GetIdentity(string googleId)
		{
			var user =
				await _context
					.Users
					.AsNoTracking()
					.FirstOrDefaultAsync(s => s.GoogleId.Equals(googleId));

			if (user is null) return null;

			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
			};

			var claimsIdentite =
				new ClaimsIdentity(
					claims,
					"Token",
					ClaimsIdentity.DefaultNameClaimType,
					ClaimsIdentity.DefaultRoleClaimType);

			_logger.LogWarning($"user:{googleId} is login at google");

			return claimsIdentite;
		}
	}
}

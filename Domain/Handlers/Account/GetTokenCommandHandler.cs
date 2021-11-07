using Common.Models.User;
using DataContext;
using Domain.Commands.Account;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Service.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Domain.Handlers.Account
{
	public class GetTokenCommandHandler : IRequestHandler<AuthenticationCommand, UserToken>
	{
		private readonly ApplicationDbContext _context;
		private readonly IMd5Hash _md5Hash;
		private readonly ILogger<GetTokenCommandHandler> _logger;

		public GetTokenCommandHandler(ApplicationDbContext context, IMd5Hash md5Hash, ILogger<GetTokenCommandHandler> logger)
		{
			_context = context;
			_md5Hash = md5Hash;
			_logger = logger;
		}

		public async Task<UserToken> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
		{
			var identity = await GetIdentity(request.Email, request.Password);

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

		private async Task<ClaimsIdentity> GetIdentity(string email, string password)
		{
			var hash = _md5Hash.GetMd5Hash(password);

			var user =
				await _context
					.Users
					.AsNoTracking()
					.FirstOrDefaultAsync(x => x.Email == email && x.PasswordHash == hash);

			if (user == null) return null;

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

			_logger?.LogWarning($"user:{email} login to email");

			return claimsIdentite;
		}
	}
}
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Handlers.Account
{
	public class AuthenticationOptions
	{
		public const string ISSUER = "viTouch_Server";
		public const string AUDIENCE = "viTouch_Client";
		private const string KEY = "c3f8a039-4e66-4861-8e76-f4efa63db952";
		public const int LIFETIME = 60;

		public static SymmetricSecurityKey GetSymmetricSecurityKey()
		{
			return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
		}
	}
}
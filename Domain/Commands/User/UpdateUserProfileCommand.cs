using MediatR;

namespace Domain.Commands.User
{
	public class UpdateUserProfileCommand: IRequest<bool>
	{
		public int UserId { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public string Email { get; set; }
		public string GoogleUserId { get; set; }
		public string ImageUrl { get; set; }
	}
}

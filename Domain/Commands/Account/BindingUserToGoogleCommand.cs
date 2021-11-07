using MediatR;

namespace Domain.Commands.Account
{
	public class BindingUserToGoogleCommand : IRequest<bool>
	{
		public int UserId { get; set; }
		public string GoogleUserId { get; set; }
		public string ImageUrl { get; set; }
	}
}

using MediatR;

namespace Domain.Commands.Video
{
	public class AddDislikeCommand : IRequest<bool>
	{
		public int VideoId { get; set; }
		public int? UserId { get; set; }
	}
}

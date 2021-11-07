using MediatR;

namespace Domain.Commands.Video
{
	public class RemoveDislikeVideoCommand : IRequest<bool>
	{
		public int VideoId { get; set; }
		public int? UserId { get; set; }
	}
}

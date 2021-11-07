using MediatR;

namespace Domain.Commands.Video
{
	public class RemoveLikeVideoCommand : IRequest<bool>
	{
		public int? UserId { get; set; }
		public int VideoId { get; set; }
	}
}

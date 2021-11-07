using MediatR;

namespace Domain.Commands.Video
{
	public class AddLikeVideoCommand : IRequest<bool>
	{
		public int VideoId { get; set; }
		public int? UserId { get; set; }
	}
}

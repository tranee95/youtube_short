using MediatR;

namespace Domain.Commands.Video
{
	public class DeleteVidioCommand : IRequest<bool>
	{
		public int VideoId { get; set; }
	}
}

using MediatR;

namespace Domain.Commands.Video
{
	public class GetVideoCommand : IRequest<Common.Models.Video.Video>
	{
		public int VideoId { get; set; }
	}
}

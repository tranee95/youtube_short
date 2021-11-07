using MediatR;

namespace Domain.Commands.Video
{
	public class GetVideoImageByIdCommand : IRequest<string>
	{
		public int Id { get; set; }
	}
}

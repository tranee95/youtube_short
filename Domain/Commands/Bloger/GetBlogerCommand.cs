using MediatR;

namespace Domain.Commands.Bloger
{
	public class GetBlogerCommand : IRequest<Common.Models.Bloger.Bloger>
	{
		public int Id { get; set; }
	}
}
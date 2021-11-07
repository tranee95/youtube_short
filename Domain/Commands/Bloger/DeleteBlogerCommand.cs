using MediatR;
using BlogerModel = Common.Models.Bloger.Bloger;

namespace Domain.Commands.Bloger
{
	public class DeleteBlogerCommand : IRequest<bool>
	{
		public int Id { get; set; }
	}
}
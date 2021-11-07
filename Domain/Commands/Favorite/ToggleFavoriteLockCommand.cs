using MediatR;

namespace Domain.Commands.Favorite
{
	public class ToggleFavoriteLockCommand : IRequest<bool>
	{
		public string Guid { get; set; }
	}
}

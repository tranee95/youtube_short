using MediatR;

namespace Domain.Commands.Bloger
{
	public class SetCreateBlogerCommand : IRequest<bool>
	{
		public string Name { get; set; }
		public string NikcName { get; set; }
		public string Url { get; set; }
		public string UrlAvatar { get; set; }
		public int Subscribers { get; set; }
	}
}
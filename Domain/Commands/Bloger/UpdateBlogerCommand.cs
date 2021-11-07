using MediatR;
using BlogerModel = Common.Models.Bloger.Bloger;

namespace Domain.Commands.Bloger
{
	public class UpdateBlogerCommand : IRequest<BlogerModel>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string NikcName { get; set; }
		public string Url { get; set; }
		public string UrlAvatar { get; set; }
		public bool Active { get; set; }
	}
}
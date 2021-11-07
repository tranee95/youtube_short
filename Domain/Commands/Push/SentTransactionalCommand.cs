using Common.Models.Push;
using MediatR;
using System.Collections.Generic;

namespace Domain.Commands.Push
{
	public class SentTransactionalCommand : IRequest<PushResult>
	{
		public int Platforms { get; set; }
		public string Title { get; set; }
		public string Body { get; set; }
		public List<string> Ids { get; set; }
	}
}

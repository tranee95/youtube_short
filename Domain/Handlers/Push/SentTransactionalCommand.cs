using Common.Models.Push;
using Domain.Commands.Push;
using MediatR;
using Service.Push;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Domain.Handlers.Push
{
	public class SentTransactionalCommandHandler : IRequestHandler<SentTransactionalCommand, PushResult>
	{
		private readonly PushBotsService _pushBotsService;
		private readonly ILogger<SentTransactionalCommandHandler> _logger;

		public SentTransactionalCommandHandler(PushBotsService pushBotsService, ILogger<SentTransactionalCommandHandler> logger)
		{
			_pushBotsService = pushBotsService;
			_logger = logger;
		}

		public async Task<PushResult> Handle(SentTransactionalCommand request, CancellationToken cancellationToken)
		{
			var transaction = new TransactionalPushContent((Platforms)request.Platforms, request.Title, request.Body, request.Ids);
			var result = await _pushBotsService.SendPushTransactionalAsync(transaction);

			foreach (var ids in transaction.Recipients.Ids)
			{
				_logger.LogWarning($"push to device: {ids}");
			}

			return result;
		}
	}
}

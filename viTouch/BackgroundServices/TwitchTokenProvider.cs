using Common.Authorization;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace viTouch.BackgroundServices
{
	public class TwitchTokenProviderBackground : BackgroundService
	{
		private readonly ITwitchTokenProvider _service;

		public TwitchTokenProviderBackground(ITwitchTokenProvider service)
		{
			_service = service;
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{

			return Task.Run(async () => await _service.UpdateToken(stoppingToken), stoppingToken);
		}
	}
}
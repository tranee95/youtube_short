using Common.Models.Push;
using System.Threading.Tasks;

namespace Service.Push
{
	public interface IPushBotsService
	{
		Task<PushResult> SendPushTransactionalAsync(TransactionalPushContent tPushContent);
	}
}


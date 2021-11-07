using System.Threading;
using System.Threading.Tasks;

namespace Common.Authorization
{
    public interface ITwitchTokenProvider
    {
        public string GetToken();
        public Task UpdateToken(CancellationToken cancellationToken);
    }
}
using Common.Models.Push;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Service.Push
{
	public class PushBotsService : IPushBotsService
	{
		private readonly HttpClient _client;

		public PushBotsService(HttpClient client)
		{
			client.BaseAddress = new Uri("https://api.pushbots.com/3/");

			client.DefaultRequestHeaders.Add("x-pushbots-appid", "5fdc44a7b1b04d4c26234b89");
			client.DefaultRequestHeaders.Add("x-pushbots-secret", "78561b35ddaf7952a3d4a58b82664b92");

			_client = client;
		}

		public async Task<PushResult> SendPushTransactionalAsync(TransactionalPushContent tPushContent)
		{
			var content = new StringContent(tPushContent.ToJson(), Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("push/transactional", content);

			var resultString = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<PushResult>(resultString);
		}
	}
}

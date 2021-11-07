using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Common.Authorization;
using Common.Models.Twitch;
using Microsoft.Extensions.Options;

namespace Service.Twitch
{
    public class TwitchService : ITwitchService
    {
        private readonly HttpClient _client;
        private readonly TwitchOptions _options;
        private readonly ITwitchTokenProvider _tokenProvider;

        private HttpRequestMessage DefaultRequest => new HttpRequestMessage()
        {
            Headers = {{"Authorization", "Bearer " + _tokenProvider.GetToken()}, {"Client-Id", _options.ClientId}}
        };

        public TwitchService(HttpClient client, IOptions<TwitchOptions> options, ITwitchTokenProvider tokenProvider)
        {
            _options = options.Value;
            _client = client;
            _client.BaseAddress = new Uri(_options.VideoApi);
            _tokenProvider = tokenProvider;
        }

        public async Task<TwitchVideoResponse> GetVideoAsync(string videoId)
        {
            var request = DefaultRequest;
            request.RequestUri = new Uri(_client.BaseAddress,$"helix/videos?id={videoId}");
            //full example https://api.twitch.tv/helix/videos?id=918656677
            var response = await _client.SendAsync(request);
            var resultString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TwitchVideoResponse>(resultString);
        }

        public async Task<TwitchChanelResponse> GetChanelAsync(string chanelId)
        {
            var request = DefaultRequest;
            request.RequestUri = new Uri(_client.BaseAddress,$"helix/users?id={chanelId}");
            //full example https://api.twitch.tv/helix/users?id=189584104
            var response = await _client.SendAsync(request);
            var resultString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TwitchChanelResponse>(resultString);
        }
    }
}
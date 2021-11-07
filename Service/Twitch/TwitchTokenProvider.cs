using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Common.Authorization;
using Common.Extensions;
using Common.Models.Twitch;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Service.Twitch
{
    public class TwitchTokenProvider : ITwitchTokenProvider
    {
        private readonly HttpClient _client;
        private readonly TwitchOptions _options;
        private TokenInfo _tokenInfo = new TokenInfo();
        private const int tokenUpdateTime = 600;

        public TwitchTokenProvider(IOptions<TwitchOptions> options)
        {
            _options = options.Value;
            _client = new HttpClient {BaseAddress = new Uri(_options.AuthApi)};
        }

        public string GetToken()
        {
            return _tokenInfo.Token;
        }

        public async Task UpdateToken(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var dto = await TokenDtoAsync(cancellationToken);
                var tokenLifetime = TimeSpan.FromSeconds(int.Parse(dto.expires_in) - tokenUpdateTime);
                _tokenInfo = new TokenInfo()
                {
                    Token = dto.access_token,
                    Expires = DateTime.Now.Add(tokenLifetime),
                    TokenLifetime = tokenLifetime
                };
                
                await Task.Delay(_tokenInfo.TokenLifetime, cancellationToken);
            }
        }

        private async Task<TokenDto> TokenDtoAsync(CancellationToken cancellationToken)
        {
            try
            {
                var uri = $"oauth2/token" +
                          $"?client_id={_options.ClientId}" +
                          $"&client_secret={_options.Secret}" +
                          $"&scope=analytics:read:extensions" +
                          $"&grant_type=client_credentials";
                var response = await _client.PostAsync(uri, null, cancellationToken);

                var resultString = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    var data = new
                    {
                        statusCode = response.StatusCode,
                        response = resultString,
                    }.ToJsonString();

                    throw new Exception($"Ошибка получения токена: {data}");
                }

                var dto = JsonConvert.DeserializeObject<TokenDto>(resultString);
                return dto;
            }
            catch (Exception e)
            {
                throw new Exception("Не удалось получить токен авторизации", e);
            }
        }

        private class TokenDto
        {
            public string access_token { get; set; }
            public string expires_in { get; set; }
            public string[] scope { get; set; }
            public string token_type { get; set; }
        }

        private class TokenInfo
        {
            public string Token { get; set; }
            public DateTime Expires { get; set; } = DateTime.Now;
            public TimeSpan TokenLifetime { get; set; } = TimeSpan.FromSeconds(1);
        }
    }
}
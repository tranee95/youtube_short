using Common.Models.YouTobe;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Service.YouTube
{
	public class YouTubeService : IYouTubeService
	{
		private readonly HttpClient _client;
		private readonly string KEY = "AIzaSyDQDvk5xWlNbHblM7matZwVOjHfRZMLIso";

		public YouTubeService(HttpClient client)
		{
			client.BaseAddress = new Uri($"https://www.googleapis.com/youtube/v3/");
			_client = client;
		}

		public async Task<YoutubeResponse> GetVideoAsync(string videoId)
		{
			var response = await _client.GetAsync($"videos?id={videoId}&key={KEY}&part=snippet,statistics");
			var resultString = await response.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<YoutubeResponse>(resultString);
		}

		public async Task<ChanelResponse> GetChanelAsync(string chanelId)
		{
			var response = await _client.GetAsync($"channels?part=snippet%2CcontentDetails&id={chanelId}&key={KEY}");
			var resultString = await response.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<ChanelResponse>(resultString);
		}
	}
}

using Common.Models.YouTobe;
using System.Threading.Tasks;

namespace Service.YouTube
{
	public interface IYouTubeService
	{
		Task<YoutubeResponse> GetVideoAsync(string videoId);
		Task<ChanelResponse> GetChanelAsync(string chanelId);
	}
}

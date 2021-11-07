using System.Threading.Tasks;
using Common.Models.Twitch;

namespace Service.Twitch
{
    public interface ITwitchService
    {
        Task<TwitchVideoResponse> GetVideoAsync(string videoId);
        Task<TwitchChanelResponse> GetChanelAsync(string chanelId);
    }
}
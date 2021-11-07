using MediatR;

namespace Domain.Commands.Twitch
{
    public class EmbedTwitchCommand : IRequest<string>
    {
        public int StartSeconds { get; set; }
        public int EndSeconds { get; set; }
        public string VideoId { get; set; }
    }
}
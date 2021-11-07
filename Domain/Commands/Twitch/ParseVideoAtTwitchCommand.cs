using System.Collections.Generic;
using MediatR;

namespace Domain.Commands.Twitch
{
    public class ParseVideoAtTwitchCommand : IRequest<Common.Models.Video.Video>
    {
        public string VideoId { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public List<int> ThemesId { get; set; }
    }
}
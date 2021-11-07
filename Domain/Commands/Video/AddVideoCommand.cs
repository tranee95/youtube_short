using MediatR;
using System.Collections.Generic;

namespace Domain.Commands.Video
{
	public class AddVideoCommand : IRequest<bool>
	{
		public int PlatformVideoId { get; set; }
		public int BlogerId { get; set; }
		public string ChanelId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Url { get; set; }
		public int StartVideoSeconds { get; set; }
		public int EndVideoSeconds { get; set; }
		public int ViewCount { get; set; }
		public int LikeCount { get; set; }
		public int DislikeCount { get; set; }
		public int CommentCount { get; set; }
		public bool Active { get; set; }
		public List<int> ThemesId { get; set; }
	}
}

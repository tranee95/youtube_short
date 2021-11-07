namespace Common.Models.Video
{
	public class Likes
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int VideoId { get; set; }
		public bool Active { get; set; }
	}
}

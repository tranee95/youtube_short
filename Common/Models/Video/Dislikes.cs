﻿namespace Common.Models.Video
{
	public class Dislikes
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int VideoId { get; set; }
		public bool Active { get; set; }
	}
}

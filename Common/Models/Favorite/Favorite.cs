using System.ComponentModel.DataAnnotations;

namespace Common.Models.Favorite
{
	public class Favorite
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Guid { get; set; }
		public int UserId { get; set; }
		[Required]
		public string Name { get; set; }
		public int[] VideosId { get; set; }
		public bool IsDefaultList { get; set; }
		public bool Active { get; set; }
		public bool IsLock { get; set; }
	}
}

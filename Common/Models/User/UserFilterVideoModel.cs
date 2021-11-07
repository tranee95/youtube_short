using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models.User
{
	public class FilterVideoModel
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public List<int> BlogersId { get; set; }
		public List<int> ThemesId { get; set; }
		public bool Active { get; set; }
	}
}

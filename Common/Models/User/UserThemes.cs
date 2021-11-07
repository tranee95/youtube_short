namespace Common.Models.User
{
	public class UserThemes
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int ThemeId { get; set; }
		public bool Active { get; set; }
	}
}

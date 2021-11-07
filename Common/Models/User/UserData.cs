namespace Common.Models.User
{
	public class UserData
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public string DisplayName { get; set; }
		public string PasswordHash { get; set; }
		public byte[] Avatar { get; set; }
		public bool Active { get; set; }
		public string Role { get; set; }
		public string GoogleId { get; set; }
		public string ImageUrl { get; set; }
		public int Points { get; set; }
	}
}
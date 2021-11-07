namespace Common.Models.User
{
	public class UserBloger
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int BlogerId { get; set; }
		public bool Active { get; set; }
	}
}

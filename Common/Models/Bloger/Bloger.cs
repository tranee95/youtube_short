namespace Common.Models.Bloger
{
	public class Bloger
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string NikcName { get; set; }
		public string Url { get; set; }
		public int Subscribers { get; set; }
		public byte[] Avatar { get; set; }
		public string UrlAvatar { get; set; }
		public bool Active { get; set; }
		public string ChanelId { get; set; }
	}
}
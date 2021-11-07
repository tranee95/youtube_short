using Newtonsoft.Json;

namespace Common.Models.Push
{
	public class PushMessage
	{
		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("body")]
		public string Body { get; set; }
	}
}

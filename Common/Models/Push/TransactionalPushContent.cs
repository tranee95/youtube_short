using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Models.Push
{
	public class TransactionalPushContent
	{
		public TransactionalPushContent(Platforms platform, string title, string body, List<string> ids)
		{
			Topic = "transactional_notification";
			Platform = platform;
			Message = new PushMessage
			{
				Title = title,
				Body = body
			};
			Recipients = new Recipients
			{
				Ids = ids
			};
		}

		[JsonProperty("topic")]
		public string Topic { get; set; }

		[JsonProperty("platform")]
		public Platforms Platform { get; set; }

		[JsonProperty("message")]
		public PushMessage Message { get; set; }

		[JsonProperty("recipients")]
		public Recipients Recipients { get; set; }

		public string ToJson() => JsonConvert.SerializeObject(this);
	}
}

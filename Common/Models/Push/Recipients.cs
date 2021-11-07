using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Models.Push
{
	public class Recipients
	{
		[JsonProperty("ids")]
		public List<string> Ids { get; set; }
	}
}

using System;

namespace Common.Models.Logging
{
	public class EventLog
	{
		public int Id { get; set; }
		public int EventId { get; set; }
		public string LogLevel { get; set; }
		public string Message { get; set; }
		public DateTime CreatedTime { get; set; }
		public string UserId { get; set; }
		public int EventCode { get; set; }
		public string Path { get; set; }
		public string Ip { get; set; }
		public string UserName { get; set; }
		public string EventMessage { get; set; }
	}
}
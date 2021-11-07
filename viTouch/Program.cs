using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace viTouch
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args)
		{
			// Свойство для удобнобного поиска логов по сервису.
			var serviceName = Assembly.GetEntryAssembly()?.GetName().Name;
			MappedDiagnosticsLogicalContext.Set("Service", serviceName);
			return Host.CreateDefaultBuilder(args)
			.ConfigureLogging(logging =>
			{
				logging.ClearProviders();
				logging.SetMinimumLevel(LogLevel.Trace);
			})
			.UseNLog()
			.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
		}
	}
}
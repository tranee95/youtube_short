using DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Service.Security;

namespace DomainTests
{
	public class DependencySetupFixture
	{
		public static ApplicationDbContext Context { get; set; }
		public ServiceProvider ServiceProvider { get; private set; }
		public ILoggerFactory LoggerFactory { get; private set; }

		public DependencySetupFixture()
		{
			//здесь как и в StartUp.cs нам нужно добавить все сервисы в сервисы провайдер
			//тогда станет возможным внедрять их как зависимости и в тестах
			var services = new ServiceCollection();
			services.AddSingleton<DbContext, ApplicationDbContext>();
			services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase(databaseName: "TestDatabase"), ServiceLifetime.Transient);
			services.AddTransient<IMd5Hash, Md5Hash>();

			//формируем сервис провайдер, который будет доступен в тестовых классах (реализующих IClassFixture<DependencySetupFixture>)
			ServiceProvider = services.BuildServiceProvider();

			Context = ServiceProvider.GetService<ApplicationDbContext>();

			//в некоторых случаях мы передаем уже внедренный логгер
			//в данном случае мы создаем фабрику, которая создат логгер в базом для тестовых классе
			LoggerFactory = new Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory();
		}
	}
}

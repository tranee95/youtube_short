using DataContext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace DomainTests
{
	public class BaseTest<T> : IClassFixture<DependencySetupFixture>
	{
		private readonly ServiceProvider _serviceProvide;
		private readonly ILogger<T> _logger;

		public ApplicationDbContext Context { get; }

		public BaseTest(DependencySetupFixture fixture)
		{
			//инициализируем сервис-провайдера для случая, когда в тестируемых обработчиках
			//в качестве параметра надо передать сервис (в обычном случае он бы был сформирован через механизм инъекций самого Asp.Net Core)
			//в данном случае сервис можно будет создать посредством вызова, например:
			//var emailSender = fixture.ServiceProvider.GetService<IEmailSender>();
			//!важно! для указанного выше примера нужно подключение using Microsoft.Extensions.DependencyInjection;
			_serviceProvide = fixture.ServiceProvider;
			Context = DependencySetupFixture.Context;

			//создаем сразу экземпляр логгера, который будет доступен наследникам через protected свойство
			_logger = fixture.LoggerFactory.CreateLogger<T>();
		}
	}
}

using Domain.Commands.User;
using Domain.Handlers.User;
using System.Threading;
using Xunit;

namespace DomainTests.User
{
	public class DeteleUserSubscrabeToBolgerCommandHandlerTest : BaseTest<DeteleUserSubscrabeToBolgerCommandHandler>
	{
		public DeteleUserSubscrabeToBolgerCommandHandlerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestDeteleUserSubscrabeToBolgerCommandHandler()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.User.UserBloger>("UserBloger");

				var command = new DeteleUserSubscrabeToBolgerCommand { UserId = 3, BlogerId = 2 };
				var handler = new DeteleUserSubscrabeToBolgerCommandHandler(context);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				Assert.True(result);
				Assert.DoesNotContain(context.UserBloger, s => s.Id.Equals(4));
			}
		}
	}
}
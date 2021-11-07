using Domain.Commands.User;
using Domain.Handlers.User;
using System.Threading;
using Xunit;

namespace DomainTests.User
{
	public class GetUserCommandHandlerTest : BaseTest<GetUserCommandHandler>
	{
		public GetUserCommandHandlerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestUserCommandHandler()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.User.UserData>("UserData");

				var command = new GetUserCommand { UserId = 1 };
				var handler = new GetUserCommandHandler(context);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				Assert.NotNull(result);
				Assert.True(result.Active);
				Assert.Equal("tranee95@gmail.com", result.Email);
			}
		}
	}
}

using Domain.Commands.Account;
using Domain.Handlers.Account;
using Service.Security;
using System.Threading;
using Xunit;

namespace DomainTests.Account
{
	public class GetTokenCommandHandlerTest : BaseTest<AuthenticationCommand>
	{
		public GetTokenCommandHandlerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestGetTokenCommandHandler()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.User.UserData>("UserData");

				var command = new AuthenticationCommand
				{
					Email = "tranee95@gmail.com",
					Password = "123",
				};

				var handler = new GetTokenCommandHandler(context, new Md5Hash(), null);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				Assert.NotNull(result);
				Assert.Equal(1, result.UserId);
				Assert.False(string.IsNullOrEmpty(result.Token));
			}
		}
	}
}

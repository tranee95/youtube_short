using Domain.Commands.Account;
using Domain.Handlers.Account;
using Service.Security;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace DomainTests.Account
{
	public class SignUpCommandHandlerTest : BaseTest<SignUpCommand>
	{
		public SignUpCommandHandlerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestSignUpCommandHandler()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.User.UserData>("UserData");

				var command = new SignUpCommand
				{
					Email = "singuptest@gmail.com",
					Password = "123456",
					ConfirmPassword = "123456"
				};

				var handler = new SignUpCommandHandler(context, new Md5Hash());

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				var user = context.Users.FirstOrDefault(s => s.Email == "singuptest@gmail.com");
				var users = context.Users.Where(s => s.Active == true);

				Assert.True(result);
				Assert.NotNull(user);
				Assert.Equal("singuptest@gmail.com", user.DisplayName);
				Assert.Equal(4, users.Count());
				Assert.NotNull(users.FirstOrDefault(s => s.Email == "singuptest@gmail.com"));
			}
		}
	}
}

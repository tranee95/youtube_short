using Domain.Commands.User;
using Domain.Handlers.User;
using System.Linq;
using System.Threading;
using Xunit;

namespace DomainTests.User
{
	public class UpdateAvataCommandHandlerTest : BaseTest<UpdateAvataCommand>
	{
		public UpdateAvataCommandHandlerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestUpdateAvataCommandHandler()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.User.UserData>("UserData");

				var command = new UpdateAvataCommand
				{
					UserId = 1,
					Avatar = new byte[1]
				};

				var handler = new UpdateAvataCommandHandler(context);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				var user = context.Users.FirstOrDefault(s => s.Id == 1);

				Assert.True(result);
				Assert.NotNull(user.Avatar);
			}
		}
	}
}

using Domain.Commands.User;
using Domain.Handlers.User;
using System.Threading;
using Xunit;

namespace DomainTests.User
{
	public class DeleteUserThemesCommandHandlerTest : BaseTest<DeleteUserThemesCommandHandler>
	{
		public DeleteUserThemesCommandHandlerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestDeleteUserThemesCommandHandler()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.User.UserThemes>("UserThemes");

				var command = new DeleteUsersThemesCommand { ThemeId = 8, UserId = 2 };
				var handler = new DeleteUserThemesCommandHandler(context);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				Assert.True(result);
				Assert.DoesNotContain(context.UserThemes, s => s.Id.Equals(7));
			}
		}
	}
}
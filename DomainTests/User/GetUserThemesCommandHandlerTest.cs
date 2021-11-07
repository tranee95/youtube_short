using Domain.Commands.User;
using Domain.Handlers.User;
using System.Linq;
using System.Threading;
using Xunit;

namespace DomainTests.User
{
	public class GetUserThemesCommandHandlerTest : BaseTest<GetUserThemesCommand>
	{
		public GetUserThemesCommandHandlerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestGetUserThemesCommandHandler()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context)
					.Seed<Common.Models.User.UserData>("UserData")
					.Seed<Common.Models.Themes.Themes>("Themes")
					.Seed<Common.Models.User.UserThemes>("UserThemes");

				var command = new GetUserThemesCommand { UserId = 2 };
				var handler = new GetUserThemesCommandHandler(context);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				Assert.NotNull(result);
				Assert.Equal(4, result.Count);
				Assert.NotNull(result.FirstOrDefault(s => s.Id == 8));
			}
		}
	}
}

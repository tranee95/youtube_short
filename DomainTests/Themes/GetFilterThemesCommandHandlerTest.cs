using Domain.Commands.Themes;
using Domain.Handlers.Themes;
using System.Linq;
using System.Threading;
using Xunit;

namespace DomainTests.Themes
{
	public class GetFilterThemesCommandHandlerTest : BaseTest<GetFilterThemesCommandHandler>
	{
		public GetFilterThemesCommandHandlerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestGetFilterThemesCommandHandler()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.Themes.Themes>("Themes");

				var command = new GetFilterThemesCommand { ThemesName = "Music" };
				var handler = new GetFilterThemesCommandHandler(context);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				Assert.NotNull(result);
				Assert.Single(result);
				Assert.Equal("Music", result.First().Name);
			}
		}
	}
}

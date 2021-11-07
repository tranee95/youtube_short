using Domain.Commands.Themes;
using Domain.Handlers.Themes;
using System.Linq;
using System.Threading;
using Xunit;

namespace DomainTests.Themes
{
	public class DeleteThemeCommandHandlerTest : BaseTest<DeleteThemeCommand>
	{
		public DeleteThemeCommandHandlerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestDeleteThemeCommandHandler()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.Themes.Themes>("Themes");

				var command = new DeleteThemeCommand { ThemeId = 6 };
				var handler = new DeleteThemeCommandHandler(context);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				var themes = context.Themes.Where(s => s.Active == true);

				Assert.True(result);
				Assert.Equal(7, themes.Count());
				Assert.Null(themes.FirstOrDefault(s => s.Id == 6));
			}
		}
	}
}

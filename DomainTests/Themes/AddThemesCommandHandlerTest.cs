using Domain.Commands.Themes;
using Domain.Handlers.Themes;
using System.Threading;
using Xunit;

namespace DomainTests.Themes
{
	public class AddThemesCommandHandlerTest : BaseTest<AddThemeCommand>
	{
		public AddThemesCommandHandlerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestThemesCommandHandlerTest()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.Bloger.Bloger>("Themes");

				var command = new AddThemeCommand { Name = "Test theme Name" };
				var handler = new AddThemesCommandHandler(context);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				Assert.NotNull(result);
				Assert.Contains(result.Name, "Test theme Name");
				Assert.True(result.Active);
			}
		}
	}
}

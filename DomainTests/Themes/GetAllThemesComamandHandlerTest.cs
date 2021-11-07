using Domain.Commands.Themes;
using Domain.Handlers.Themes;
using System.Threading;
using Xunit;

namespace DomainTests.Themes
{
	public class GetAllThemesComamandHandlerTest : BaseTest<GetAllThemesComamand>
	{
		public GetAllThemesComamandHandlerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestGetAllThemesComamandHandler()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.Themes.Themes>("Themes");

				var command = new GetAllThemesComamand();
				var handler = new GetAllThemesComamandHandler(context);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				Assert.NotNull(result);
				Assert.Equal(8, result.Count);
			}
		}
	}
}

using System.Threading;
using Domain.Commands.Bloger;
using Domain.Handlers.Bloger;
using Xunit;

namespace DomainTests.Bloger
{
	public class GetBlogerCommandHandlerTest : BaseTest<GetBlogerCommandHandler>
	{
		public GetBlogerCommandHandlerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestGetBlogerCommandHandler()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.Bloger.Bloger>("Bloger");

				var command = new GetBlogerCommand {Id = 1};
				var handler = new GetBlogerCommandHandler(context);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				Assert.NotNull(result);
				Assert.Equal(1, result.Id);
				Assert.Equal("Bloger", result.Name);
			}
		}
	}
}
using Domain.Commands.Bloger;
using Domain.Handlers.Bloger;
using System.Threading;
using Xunit;

namespace DomainTests.Bloger
{
	public class GetFilterBlogersCommandHandlerTest : BaseTest<GetFilterBlogersCommandHandler>
	{
		public GetFilterBlogersCommandHandlerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestGetFilterBlogersCommandHandler()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.Bloger.Bloger>("Bloger");

				var command = new GetFilterBlogersCommand { BlogerName = "Beacon" };
				var handler = new GetFilterBlogersCommandHandler(context);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				Assert.NotNull(result);
				Assert.Single(result);
				Assert.Contains(context.Blogers, s => s.Id.Equals(2));
			}
		}
	}
}

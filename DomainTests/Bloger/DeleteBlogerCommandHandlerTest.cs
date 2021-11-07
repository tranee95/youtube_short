using System.Threading;
using Domain.Commands.Bloger;
using Domain.Handlers.Bloger;
using Xunit;

namespace DomainTests.Bloger
{
	public class DeleteBlogerCommandHandlerTest : BaseTest<DeleteBlogerCommandHandler>
	{
		public DeleteBlogerCommandHandlerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestDeleteBlogerCommandHandler()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.Bloger.Bloger>("Bloger");

				var command = new DeleteBlogerCommand {Id = 3};
				var handler = new DeleteBlogerCommandHandler(context);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				Assert.True(result);
				Assert.DoesNotContain(context.Blogers, s => s.Id == 3);
			}
		}
	}
}
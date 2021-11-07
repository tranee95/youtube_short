using Domain.Commands.Bloger;
using Domain.Handlers.Bloger;
using System.Linq;
using System.Threading;
using Xunit;
using BlogerModel = Common.Models.Bloger.Bloger;

namespace DomainTests.Bloger
{
	public class SetCreateBlogerCommandHandlerTest : BaseTest<SetCreateBlogerCommandHandler>
	{
		public SetCreateBlogerCommandHandlerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestSetCreateBlogerCommandHandler()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<BlogerModel>("Bloger");

				var command = new SetCreateBlogerCommand
				{
					Name = "this create test bloger",
					NikcName = "test nick Name",
					Subscribers = 23341,
				};
				var handler = new SetCreateBlogerCommandHandler(context);

				var result = handler.Handle(command, new CancellationTokenSource().Token);

				await context.SaveChangesAsync();

				Assert.Equal(5, context.Blogers.Count());
				Assert.Contains(context.Blogers, s => s.Id == 5);
			}
		}
	}
}
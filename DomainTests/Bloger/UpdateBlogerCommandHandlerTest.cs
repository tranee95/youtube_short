using Domain.Commands.Bloger;
using Domain.Handlers.Bloger;
using System.Threading;
using Xunit;

namespace DomainTests.Bloger
{
	public class UpdateBlogerCommandHandlerTest : BaseTest<UpdateBlogerCommand>
	{
		public UpdateBlogerCommandHandlerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestUpdateBlogerCommandHandler()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.Bloger.Bloger>("Bloger");

				var command = new UpdateBlogerCommand
				{
					Id = 1,
					Active = false,
					Name = "update test",
					NikcName = "update test",
					Url = "url test"
				};

				var handler = new UpdateBlogerCommandHandler(context);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				Assert.NotNull(result);
				Assert.Equal(1, result.Id);
				Assert.False(result.Active);
				Assert.Equal("update test", result.Name);
				Assert.Equal("update test", result.NikcName);
				Assert.Equal("url test", result.Url);
			}
		}
	}
}

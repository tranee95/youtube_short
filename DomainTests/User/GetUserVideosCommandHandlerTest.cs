using System.Linq;
using Domain.Commands.User;
using Domain.Handlers.User;
using System.Threading;
using Xunit;

namespace DomainTests.User
{
	public class GetUserVideosCommandHandlerTest : BaseTest<GetUserVideosCommandHandler>
	{
		public GetUserVideosCommandHandlerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestGetUserVideosCommandHandler()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.Video.Video>("Video");

				var command = new GetUserVideosCommand
				{
					Page = 1,
					Take = 5,
					UserId = 2
				};

				var handler = new GetUserVideosCommandHandler(context, null);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				Assert.NotNull(result);
				Assert.Equal(5, result.Count);
				Assert.NotNull(result.FirstOrDefault(s => s.Id.Equals(4)));
				Assert.Equal(11007, result.FirstOrDefault(s => s.Id.Equals(11)).LikeCount);
			}
		}
	}
}

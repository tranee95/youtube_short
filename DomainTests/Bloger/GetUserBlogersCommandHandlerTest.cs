using Domain.Commands.Bloger;
using Domain.Handlers.Bloger;
using System.Threading;
using Xunit;

namespace DomainTests.Bloger
{
	public class GetUserBlogersCommandHandlerTest : BaseTest<GetUserBlogersCommandHandler>
	{
		public GetUserBlogersCommandHandlerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestGetUserBlogersCommandHandler()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context)
					.Seed<Common.Models.Bloger.Bloger>("Bloger")
					.Seed<Common.Models.User.UserData>("UserData")
					.Seed<Common.Models.User.UserBloger>("UserBloger");

				var command = new GetUserBlogersCommand {Count = 10, UserId = 1};
				var handler = new GetUserBlogersCommandHandler(context);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				Assert.NotNull(result);
				Assert.Equal(2, result.Count);
			}
		}
	}
}

using Domain.Commands.Video;
using Domain.Handlers.Video;
using System.Linq;
using System.Threading;
using Xunit;

namespace DomainTests.Video
{
	public class RemoveLikeVideoCommandHandlerTest : BaseTest<RemoveLikeVideoCommandHandler>
	{
		public RemoveLikeVideoCommandHandlerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestRemoveLikeVideoCommandHandler()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.Video.Video>("Video");

				var command = new RemoveLikeVideoCommand { VideoId = 22 };
				var handler = new RemoveLikeVideoCommandHandler(context);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				Assert.True(result);
				Assert.Equal(56000, context.Videos.FirstOrDefault(s => s.Id.Equals(22)).LikeCount);
			}
		}
	}
}
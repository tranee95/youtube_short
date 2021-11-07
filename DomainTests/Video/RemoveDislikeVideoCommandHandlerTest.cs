using Domain.Commands.Video;
using Domain.Handlers.Video;
using System.Linq;
using System.Threading;
using Xunit;

namespace DomainTests.Video
{
	public class RemoveDislikeVideoCommandHandlerTest : BaseTest<RemoveDislikeVideoCommandHandler>
	{
		public RemoveDislikeVideoCommandHandlerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestRemoveDislikeVideoCommandHandler()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.Video.Video>("Video");

				var command = new RemoveDislikeVideoCommand { VideoId = 17 };
				var handler = new RemoveDislikeVideoCommandHandler(context);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				Assert.True(result);
				Assert.Equal(1903, context.Videos.FirstOrDefault(s => s.Id.Equals(17)).DislikeCount);
			}
		}
	}
}
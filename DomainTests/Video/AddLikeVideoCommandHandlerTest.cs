using System.Linq;
using Domain.Commands.Video;
using Domain.Handlers.Video;
using System.Threading;
using Xunit;

namespace DomainTests.Video
{
	public class AddLikeVideoCommandHandlerTest : BaseTest<AddLikeVideoCommandHandler>
	{
		public AddLikeVideoCommandHandlerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestAddLikeVideoCommandHandler()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.Video.Video>("Video");

				var command = new AddLikeVideoCommand { VideoId = 4};
				var handler = new AddLikeVideoCommandHandler(context);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				Assert.True(result);
				Assert.Equal(184005, context.Videos.FirstOrDefault(s => s.Id.Equals(4)).LikeCount);
			}
		}
	}
}

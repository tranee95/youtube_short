using Domain.Commands.Video;
using Domain.Handlers.Video;
using System.Linq;
using System.Threading;
using Xunit;

namespace DomainTests.Video
{
	public class AddDislikeCommandHandlerTest : BaseTest<AddDislikeCommandHandler>
	{
		public AddDislikeCommandHandlerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestAddDislikeCommandHandler()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.Video.Video>("Video");

				var command = new AddDislikeCommand { VideoId = 9 };
				var handler = new AddDislikeCommandHandler(context);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				Assert.True(result);
				Assert.Equal(112, context.Videos.FirstOrDefault(s => s.Id.Equals(9)).DislikeCount);
			}
		}
	}
}
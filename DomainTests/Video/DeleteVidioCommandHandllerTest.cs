using System.Linq;
using Domain.Commands.Video;
using Domain.Handlers.Video;
using System.Threading;
using Xunit;

namespace DomainTests.Video
{
	public class DeleteVidioCommandHandllerTest : BaseTest<DeleteVidioCommandHandller>
	{
		public DeleteVidioCommandHandllerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestDeleteVidioCommandHandller()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.Video.Video>("Video");

				var command = new DeleteVidioCommand { VideoId = 14 };
				var handler = new DeleteVidioCommandHandller(context);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				Assert.True(result);
				Assert.Null(context.Videos.FirstOrDefault(s => s.Id.Equals(14)));
			}
		}
	}
}
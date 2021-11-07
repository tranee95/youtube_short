using Domain.Commands.Video;
using Domain.Handlers.Video;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace DomainTests.Video
{
	public class GetVideosRangeCommandHandelrTest : BaseTest<GetVideosRangeCommandHandelr>
	{
		public GetVideosRangeCommandHandelrTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestUserCommandHandler()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.Video.Video>("Video");

				var command = new GetVideosRangeCommand { Videos = new List<int> { 5, 16, 8, 9, 22 } };
				var handler = new GetVideosRangeCommandHandelr(context, null);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				Assert.NotNull(result);
				Assert.Equal(5, result.Count);
				Assert.Equal(22, result.OrderBy(s => s.Id).Last().Id);
			}
		}
	}
}
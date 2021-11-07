using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Domain.Commands.Video;
using Domain.Handlers.Video;
using Xunit;

namespace DomainTests.Video
{
	public class GetVideoCommandHandllerTest : BaseTest<GetVideoCommandHandller>
	{
		public GetVideoCommandHandllerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestUserCommandHandler()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.Video.Video>("Video");

				var command = new GetVideoCommand { VideoId = 17};
				var handler = new GetVideoCommandHandller(context);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				Assert.NotNull(result);
				Assert.Equal(85000, result.CommentCount);
				Assert.Equal(967183, result.ViewCount);
			}
		}
	}
}


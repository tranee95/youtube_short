using Domain.Commands.Video;
using Domain.Handlers.Video;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace DomainTests.Video
{
	public class AddVideoCommandHandllerTest : BaseTest<AddVideoCommandHandller>
	{
		public AddVideoCommandHandllerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestAddVideoCommandHandller()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.User.UserData>("UserData");

				var command = new AddVideoCommand
				{
					PlatformVideoId = 1,
					BlogerId = 2,
					ThemesId = new List<int>() { 1, 2 },
					ChanelId = "0",
					CommentCount = 15453,
					Url = "url",
					Name = "name",
					Description = "test des",
					DislikeCount = 1234,
					LikeCount = 12334,
					ViewCount = 412341,
					StartVideoSeconds = 5,
					EndVideoSeconds = 123,
					Active = true
				};

				var handler = new AddVideoCommandHandller(context);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				Assert.True(result);
				Assert.Equal("test des", context.Videos.FirstOrDefault(s => s.DislikeCount.Equals(1234)).Description);
			}
		}
	}
}

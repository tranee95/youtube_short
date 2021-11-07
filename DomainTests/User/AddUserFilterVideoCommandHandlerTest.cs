using Domain.Commands.User;
using Domain.Handlers.User;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace DomainTests.User
{
	public class AddUserFilterVideoCommandHandlerTest : BaseTest<AddUserFilterVideoCommandHandler>
	{
		public AddUserFilterVideoCommandHandlerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestAddUserFilterVideoCommandHandler()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.User.FilterVideoModel>("FilterVideoModel");

				var command = new AddUserFilterVideoCommand
				{
					UserId = 1,
					ThemesId = new List<int> { 1, 2 },
					BlogerId = new List<int> { 3, 1 }
				};

				var handler = new AddUserFilterVideoCommandHandler(context);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				Assert.NotNull(result);
				Assert.Equal(1, result.UserId);
				Assert.Equal(3, context.UserFilterVideos.Count(s => s.UserId.Equals(1)));
			}
		}
	}
}

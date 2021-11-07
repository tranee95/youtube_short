using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Domain.Commands.User;
using Domain.Handlers.User;
using Xunit;

namespace DomainTests.User
{
	public class GetUserAvatarCommandHandlerTest : BaseTest<GetUserAvatarCommandHandlerTest>
	{
		public GetUserAvatarCommandHandlerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestGetUserAvatarCommandHandler()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.User.UserData>("UserData");

				var command = new GetUserAvatarCommand { UserId = 2};
				var handler = new GetUserAvatarCommandHandler(context);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				Assert.NotNull(result);
				Assert.Equal(6, result.Length);
			}
		}
	}
}
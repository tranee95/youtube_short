using Domain.Commands.User;
using Domain.Handlers.User;
using Service.Security;
using System.Linq;
using System.Threading;
using Xunit;

namespace DomainTests.User
{
	public class UpdateUserProfileCommandHandlerTest : BaseTest<UpdateUserProfileCommand>
	{
		public UpdateUserProfileCommandHandlerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestUpdateUserProfileCommandHandler()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.User.UserData>("UserData");
				var hashService = new Md5Hash();

				var command = new UpdateUserProfileCommand
				{
					UserId = 1,
					Email = "testUpdateProfile@gmail.com",
					ConfirmPassword = "123456",
					Password = "123456"
				};

				var handler = new UpdateUserProfileCommandHandler(context, hashService);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				var user = context.Users.FirstOrDefault(s => s.Id == 1);
				var hash = hashService.GetMd5Hash("123456");

				Assert.True(result);
				Assert.Equal("testUpdateProfile@gmail.com", user.Email);
				Assert.Equal(user.PasswordHash, hash);
			}
		}
	}
}

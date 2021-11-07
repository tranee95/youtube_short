using System.Collections.Generic;
using System.Linq;
using Domain.Commands.User;
using Domain.Handlers.User;
using System.Threading;
using Xunit;

namespace DomainTests.User
{
	public class AddUserThemesComandHandlerTest : BaseTest<AddUserThemesCommandHandler>
	{
		public AddUserThemesComandHandlerTest(DependencySetupFixture fixture) : base(fixture) { }

		[Fact]
		public async void TestUserThemesComandHandlerTest()
		{
			using (var context = ApplicationDbTestContextFactory.CreateBase())
			{
				new SeedBuilder(context).Seed<Common.Models.User.UserThemes>("UserThemes");

				var command = new AddUserThemesCommand
				{
					UserId = 3,
					Themes = new List<Common.Models.Themes.Themes>
					{
						new Common.Models.Themes.Themes
						{
							Id = 2,
							Name = "Games",
							Active = true
						}
					}
				};
				var handler = new AddUserThemesCommandHandler(context);

				var result = await handler.Handle(command, new CancellationTokenSource().Token);

				Assert.True(result);
				Assert.Single(context.UserThemes.Where(s => s.UserId.Equals(3)));
			}
		}
	}
}

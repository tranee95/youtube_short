using System;
using DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DomainTests
{
	public class ApplicationDbTestContextFactory
	{
		public static ApplicationDbContext CreateBase()
		{
			var options = new DbContextOptionsBuilder<ApplicationDbContext>()
			              .UseInMemoryDatabase(Guid.NewGuid().ToString())
			              .ConfigureWarnings(s => s.Ignore(InMemoryEventId.TransactionIgnoredWarning))
			              .Options;

			var context = new ApplicationDbContext(options, true);
			
			return context;
		}
	}
}

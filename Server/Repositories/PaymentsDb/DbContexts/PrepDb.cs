using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Repositories.PaymentsDb.DbContexts
{
	public static class PrepDb
	{
		public static void PrepPopulation(IApplicationBuilder app)
		{
			using (var serviceScope = app.ApplicationServices.CreateScope())
			{
				SeedData(serviceScope.ServiceProvider.GetService<PaymentsDbContext>());
			}
		}

		public static void SeedData(PaymentsDbContext context)
		{
			System.Console.WriteLine("Applying Migrations...");

			context.Database.Migrate();
		}
	}
}

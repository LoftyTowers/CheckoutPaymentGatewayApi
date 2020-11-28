using Microsoft.EntityFrameworkCore;
using Repositories.PaymentsDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories.PaymentsDb.DbContexts
{
	public class PaymentsDbContext : DbContext
	{
		public PaymentsDbContext(DbContextOptions<PaymentsDbContext> options) : base(options)
		{

		}

		public DbSet<User> Users { get; set; }
		public DbSet<Payment> Payments { get; set; }
		public DbSet<PaymentStatus> PaymentStatus { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().ToTable("User");
			modelBuilder.Entity<Payment>().ToTable("Payment").Property(e => e.PaymentStatusId).HasConversion<int>();
			modelBuilder.Entity<PaymentStatus>().ToTable("PaymentStatus").Property(e => e.Id).HasConversion<int>();
			modelBuilder.Entity<PaymentStatus>().HasData(
				Enum.GetValues(typeof(Common.Enums.PaymentStatus))
					.Cast<Common.Enums.PaymentStatus>()
					.Select(s => new PaymentStatus()
					{
						Id = s,
						StatusDesc = s.ToString()
					}));
		}
	}
}

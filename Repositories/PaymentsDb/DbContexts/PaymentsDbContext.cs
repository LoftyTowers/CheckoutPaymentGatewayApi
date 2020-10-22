using Microsoft.EntityFrameworkCore;
using Repositories.PaymentsDb.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.PaymentsDb.DbContexts
{
	public class PaymentsDbContext : DbContext
	{
		public PaymentsDbContext(DbContextOptions<PaymentsDbContext> options) : base(options)
		{
		}

		public DbSet<Card> Cards { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Payment> Payments { get; set; }
		public DbSet<PaymentStatus> PaymentStatus { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Card>().ToTable("Card");
			modelBuilder.Entity<User>().ToTable("User");
			modelBuilder.Entity<Payment>().ToTable("Payment");
			modelBuilder.Entity<PaymentStatus>().ToTable("PaymentStatus");
		}
	}
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repositories.PaymentsDb.DbContexts;

namespace Repositories.Migrations
{
    [DbContext(typeof(PaymentsDbContext))]
    partial class PaymentsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Repositories.PaymentsDb.Models.Card", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BankName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CVC")
                        .HasColumnType("int");

                    b.Property<string>("CardNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("Repositories.PaymentsDb.Models.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("BankPaymentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CurrencyCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSuccessful")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaymentStatusId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RequestCompleted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.HasIndex("PaymentStatusId");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("Repositories.PaymentsDb.Models.PaymentStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("StatusDesc")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PaymentStatus");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            StatusDesc = "Unknown"
                        },
                        new
                        {
                            Id = 10,
                            StatusDesc = "RequestRecieved"
                        },
                        new
                        {
                            Id = 20,
                            StatusDesc = "RequestSent"
                        },
                        new
                        {
                            Id = 30,
                            StatusDesc = "RequestSucceded"
                        },
                        new
                        {
                            Id = 999,
                            StatusDesc = "RequestFailed"
                        },
                        new
                        {
                            Id = 1009,
                            StatusDesc = "DuplicateRequest"
                        },
                        new
                        {
                            Id = 1019,
                            StatusDesc = "RequestDoesNotExist"
                        },
                        new
                        {
                            Id = 1029,
                            StatusDesc = "InsuffucentFunds"
                        },
                        new
                        {
                            Id = 1039,
                            StatusDesc = "CardNotActivated"
                        },
                        new
                        {
                            Id = 1049,
                            StatusDesc = "StolenCancelled"
                        },
                        new
                        {
                            Id = 1059,
                            StatusDesc = "InvalidCardCredentials"
                        },
                        new
                        {
                            Id = 1069,
                            StatusDesc = "CardExpired"
                        },
                        new
                        {
                            Id = 1100,
                            StatusDesc = "PaymentNotStored"
                        },
                        new
                        {
                            Id = 9999,
                            StatusDesc = "Error"
                        });
                });

            modelBuilder.Entity("Repositories.PaymentsDb.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Fullname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Repositories.PaymentsDb.Models.Card", b =>
                {
                    b.HasOne("Repositories.PaymentsDb.Models.User", null)
                        .WithMany("Cards")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Repositories.PaymentsDb.Models.Payment", b =>
                {
                    b.HasOne("Repositories.PaymentsDb.Models.Card", null)
                        .WithMany("Payments")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repositories.PaymentsDb.Models.PaymentStatus", null)
                        .WithMany("Payments")
                        .HasForeignKey("PaymentStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Repositories.PaymentsDb.Models.Card", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Repositories.PaymentsDb.Models.PaymentStatus", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Repositories.PaymentsDb.Models.User", b =>
                {
                    b.Navigation("Cards");
                });
#pragma warning restore 612, 618
        }
    }
}

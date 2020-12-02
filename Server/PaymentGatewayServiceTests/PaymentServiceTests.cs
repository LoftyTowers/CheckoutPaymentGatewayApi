using AutoMapper;
using Common.Enums;
using Common.Models;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PaymentGatewayService;
using PaymentGatewayService.Interfaces;
using PaymentGatewayService.Services;
using Repositories.PaymentsDb.Interfaces;
using System;
using Microsoft.Extensions.Logging.Abstractions;
using Autofac.Features.Indexed;
using Autofac;
using PaymentGatewayService.BankEndpoints;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace PaymentGatewayServiceTests
{
	[TestFixture]
	public class PaymentServiceTests
	{
		#region Setup

		[SetUp]
		public void SetUp()
		{
			var MockRepository = new MockRepository(MockBehavior.Strict);

			var MockLogger = MockRepository.Create<ILogger<PaymentService>>(MockBehavior.Loose);
			var MockPaymentRepo = MockRepository.Create<IPaymentRepo>(MockBehavior.Loose);

			var MockTestBankEndpoint = MockRepository.Create<IBankEndpoint>();


			#region MockPaymentRepo

			MockPaymentRepo.Setup(m => m.GetPayment(It.Is<Guid>(i => i == Guid.Parse("e4196f31-3019-456d-96d7-af6d6cc84be7"))))
				.Returns((Guid paymentId) => new Payment
				{
					Amount = 10,
					CardExpiryDate = DateTime.Now,
					CardNumber = 5425233430109903,
					CurrencyCode = "GDP",
					CVC = 132,
					FullName = "John Doe",
					IsSuccessful = true,
					PaymentId = paymentId,
					RequestDate = DateTime.Now.AddDays(-1),
					Status = PaymentStatus.RequestSucceded,
					RecievingBankName = "TestBank"
				});
			MockPaymentRepo.Setup(m => m.GetPayment(It.Is<Guid>(i => i == Guid.Parse("21e91399-40f7-41a4-992f-429effe7a1b8"))))
				 .Returns((Guid paymentId) => new Payment
				 {
					 PaymentId = paymentId,
					 IsSuccessful = false,
					 Status = PaymentStatus.Error
				 });
			MockPaymentRepo.Setup(m => m.AddUser(It.IsAny<User>()))
				.Returns<User>(user => user);
			MockPaymentRepo.Setup(m => m.StorePayment(It.IsAny<Payment>())).Returns<Payment>(p => p);
			MockPaymentRepo.Setup(m => m.UpdatePayment(It.IsAny<Payment>()));

			#endregion

			#region MockBankEndpoint

			MockTestBankEndpoint.Setup(m => m.SendPayment(
				It.Is<Payment>(payment => payment.CardNumber == 374245455400126)))
				.Returns<Payment>(payment => payment = new Payment
				{
					Status = PaymentStatus.InsuffucentFunds,
					IsSuccessful = false,
					Message = "Card Declined: Insuffucent Funds"
				});
			MockTestBankEndpoint.Setup(m => m.SendPayment(
				It.Is<Payment>(payment => payment.CardNumber == 378282246310005)))
				.Returns<Payment>(payment => payment = new Payment
				{
					Status = PaymentStatus.CardNotActivated,
					IsSuccessful = false,
					Message = "Card Declined: Card Not Activated"
				});
			MockTestBankEndpoint.Setup(m => m.SendPayment(
				It.Is<Payment>(payment => payment.CardNumber == 6250941006528599)))
				.Returns<Payment>(payment => payment = new Payment
				{
					Status = PaymentStatus.StolenCancelled,
					IsSuccessful = false,
					Message = "Card Declined: Stolen/Cancelled"
				});
			MockTestBankEndpoint.Setup(m => m.SendPayment(
				It.Is<Payment>(payment => payment.CardNumber == 60115564485789458)))
				.Returns<Payment>(payment => payment = new Payment
				{
					Status = PaymentStatus.InvalidCardCredentials,
					IsSuccessful = false,
					Message = "Card Declined: Invalid Card Credentials"
				});
			MockTestBankEndpoint.Setup(m => m.SendPayment(
				It.Is<Payment>(payment => payment.CardNumber == 6011000991300009)))
				.Returns<Payment>(payment => payment = new Payment
				{
					Status = PaymentStatus.CardExpired,
					IsSuccessful = false,
					Message = "Card Declined: Card Expired"
				});
			MockTestBankEndpoint.Setup(m => m.SendPayment(
				It.Is<Payment>(payment => payment.CardNumber == 3566000020000410)))
				.Returns<Payment>(payment => payment = new Payment
				{
					Status = PaymentStatus.Error,
					IsSuccessful = false,
					Message = "Internal Error: Please try again later or contact support."
				});
			MockTestBankEndpoint.Setup(m => m.SendPayment(
				It.Is<Payment>(payment => payment.CardNumber == 5425233430109903)))
				.Returns<Payment>(payment => payment = new Payment
				{
					Status = PaymentStatus.RequestSucceded,
					IsSuccessful = true,
					Message = string.Empty
				});

			#endregion

			MockRepository = new MockRepository(MockBehavior.Strict) { DefaultValue = DefaultValue.Empty };


			///////////////////////////////////
			///////////////////////////////////
			///////////////////////////////////
			///////////////////////////////////
			var builder = new ContainerBuilder();

			builder.RegisterInstance(MockTestBankEndpoint.Object).AsImplementedInterfaces().Keyed<IBankEndpoint>("TestBank");
			builder.RegisterInstance(MockLogger.Object).AsImplementedInterfaces();
			builder.RegisterInstance(MockPaymentRepo.Object).AsImplementedInterfaces();
			builder.RegisterType<PaymentService>().AsImplementedInterfaces();

			Container = builder.Build();

			Service = Container.Resolve<IPaymentService>();
		}

		#endregion

		#region StorePaymentTests

		[Test, Category("Service Store Payment Tests")]
		public void ProcessPayment_PaymentSentToBank_PaymentSuccess()
		{
			// Arrange
			var paymentRequest = new Payment
			{
				Amount = 10,
				CardExpiryDate = DateTime.Now,
				CardNumber = 5425233430109903,
				CurrencyCode = "GDP",
				CVC = 132,
				FullName = "John Doe",
				IsSuccessful = false,
				PaymentId = Guid.NewGuid(),
				RequestDate = DateTime.Now,
				Status = PaymentStatus.RequestRecieved,
				Card = new Card
				{
					BankName = "TestBank",
					CardNumber = 5425233430109903,
					CVC = 132,
					ExpiryDate = DateTime.Now,
					Id = Guid.NewGuid()
				},
				User = new User
				{
					Id = Guid.NewGuid(),
					DateOfBirth = DateTime.Now.AddYears(-22),
					Fullname = "John Doe"
				},
				RecievingBankName = "TestBank"
			};

			// Act
			var paymentResponse = Service.ProcessPayment(
				paymentRequest);

			// Assert
			Assert.IsNotNull(paymentResponse);
			Assert.IsTrue(paymentResponse.IsSuccessful);
			Assert.AreEqual(PaymentStatus.RequestSucceded, paymentResponse.Status);

		}

		[Test, Category("Service Store Payment Tests")]
		public void ProcessPayment_PaymentSentToBank_InsufficantFunds()
		{
			// Arrange
			var paymentRequest = new Payment
			{
				Amount = 10,
				CardExpiryDate = DateTime.Now,
				CardNumber = 374245455400126,
				CurrencyCode = "GDP",
				CVC = 132,
				FullName = "John Doe",
				IsSuccessful = false,
				PaymentId = Guid.NewGuid(),
				RequestDate = DateTime.Now,
				Status = PaymentStatus.RequestRecieved,
				Card = new Card
				{
					BankName = "TestBank",
					CardNumber = 374245455400126,
					CVC = 132,
					ExpiryDate = DateTime.Now,
					Id = Guid.NewGuid()
				},
				User = new User
				{
					Id = Guid.NewGuid(),
					DateOfBirth = DateTime.Now.AddYears(-22),
					Fullname = "John Doe"
				},
				RecievingBankName = "TestBank"
			};

			// Act
			var paymentResponse = Service.ProcessPayment(
				paymentRequest);

			// Assert
			Assert.IsNotNull(paymentResponse);
			Assert.IsFalse(paymentResponse.IsSuccessful);
			Assert.AreEqual(PaymentStatus.InsuffucentFunds, paymentResponse.Status);
			Assert.AreEqual("Card Declined: Insuffucent Funds", paymentResponse.Message);

		}

		[Test, Category("Service Store Payment Tests")]
		public void ProcessPayment_PaymentSentToBank_CardNotActive()
		{
			// Arrange
			var paymentRequest = new Payment
			{
				Amount = 10,
				CardExpiryDate = DateTime.Now,
				CardNumber = 378282246310005,
				CurrencyCode = "GDP",
				CVC = 132,
				FullName = "John Doe",
				IsSuccessful = false,
				PaymentId = Guid.NewGuid(),
				RequestDate = DateTime.Now,
				Status = PaymentStatus.RequestRecieved,
				Card = new Card
				{
					BankName = "TestBank",
					CardNumber = 378282246310005,
					CVC = 132,
					ExpiryDate = DateTime.Now,
					Id = Guid.NewGuid()
				},
				User = new User
				{
					Id = Guid.NewGuid(),
					DateOfBirth = DateTime.Now.AddYears(-22),
					Fullname = "John Doe"
				},
				RecievingBankName = "TestBank"
			};

			// Act
			var paymentResponse = Service.ProcessPayment(
				paymentRequest);

			// Assert
			Assert.IsNotNull(paymentResponse);
			Assert.IsFalse(paymentResponse.IsSuccessful);
			Assert.AreEqual(PaymentStatus.CardNotActivated, paymentResponse.Status);
			Assert.AreEqual("Card Declined: Card Not Activated", paymentResponse.Message);

		}

		[Test, Category("Service Store Payment Tests")]
		public void ProcessPayment_PaymentSentToBank_CardStolenCancelled()
		{
			// Arrange
			var paymentRequest = new Payment
			{
				Amount = 10,
				CardExpiryDate = DateTime.Now,
				CardNumber = 6250941006528599,
				CurrencyCode = "GDP",
				CVC = 132,
				FullName = "John Doe",
				IsSuccessful = false,
				PaymentId = Guid.NewGuid(),
				RequestDate = DateTime.Now,
				Status = PaymentStatus.RequestRecieved,
				Card = new Card
				{
					BankName = "TestBank",
					CardNumber = 6250941006528599,
					CVC = 132,
					ExpiryDate = DateTime.Now,
					Id = Guid.NewGuid()
				},
				User = new User
				{
					Id = Guid.NewGuid(),
					DateOfBirth = DateTime.Now.AddYears(-22),
					Fullname = "John Doe"
				},
				RecievingBankName = "TestBank"
			};

			// Act
			var paymentResponse = Service.ProcessPayment(
				paymentRequest);

			// Assert
			Assert.IsNotNull(paymentResponse);
			Assert.IsFalse(paymentResponse.IsSuccessful);
			Assert.AreEqual(PaymentStatus.StolenCancelled, paymentResponse.Status);
			Assert.AreEqual("Card Declined: Stolen/Cancelled", paymentResponse.Message);

		}

		[Test, Category("Service Store Payment Tests")]
		public void ProcessPayment_PaymentSentToBank_InvalidCardCredentials()
		{
			// Arrange
			var paymentRequest = new Payment
			{
				Amount = 10,
				CardExpiryDate = DateTime.Now,
				CardNumber = 60115564485789458,
				CurrencyCode = "GDP",
				CVC = 132,
				FullName = "John Doe",
				IsSuccessful = false,
				PaymentId = Guid.NewGuid(),
				RequestDate = DateTime.Now,
				Status = PaymentStatus.RequestRecieved,
				Card = new Card
				{
					BankName = "TestBank",
					CardNumber = 60115564485789458,
					CVC = 132,
					ExpiryDate = DateTime.Now,
					Id = Guid.NewGuid()
				},
				User = new User
				{
					Id = Guid.NewGuid(),
					DateOfBirth = DateTime.Now.AddYears(-22),
					Fullname = "John Doe"
				},
				RecievingBankName = "TestBank"
			};

			// Act
			var paymentResponse = Service.ProcessPayment(
				paymentRequest);

			// Assert
			Assert.IsNotNull(paymentResponse);
			Assert.IsFalse(paymentResponse.IsSuccessful);
			Assert.AreEqual(PaymentStatus.InvalidCardCredentials, paymentResponse.Status);
			Assert.AreEqual("Card Declined: Invalid Card Credentials", paymentResponse.Message);

		}

		[Test, Category("Service Store Payment Tests")]
		public void ProcessPayment_PaymentSentToBank_CardExpired()
		{
			// Arrange
			var paymentRequest = new Payment
			{
				Amount = 10,
				CardExpiryDate = DateTime.Now,
				CardNumber = 6011000991300009,
				CurrencyCode = "GDP",
				CVC = 132,
				FullName = "John Doe",
				IsSuccessful = false,
				PaymentId = Guid.NewGuid(),
				RequestDate = DateTime.Now,
				Status = PaymentStatus.RequestRecieved,
				Card = new Card
				{
					BankName = "TestBank",
					CardNumber = 6011000991300009,
					CVC = 132,
					ExpiryDate = DateTime.Now,
					Id = Guid.NewGuid()
				},
				User = new User
				{
					Id = Guid.NewGuid(),
					DateOfBirth = DateTime.Now.AddYears(-22),
					Fullname = "John Doe"
				},
				RecievingBankName = "TestBank"
			};

			// Act
			var paymentResponse = Service.ProcessPayment(
				paymentRequest);

			// Assert
			Assert.IsNotNull(paymentResponse);
			Assert.IsFalse(paymentResponse.IsSuccessful);
			Assert.AreEqual(PaymentStatus.CardExpired, paymentResponse.Status);
			Assert.AreEqual("Card Declined: Card Expired", paymentResponse.Message);

		}

		[Test, Category("Service Store Payment Tests")]
		public void ProcessPayment_PaymentSentToBank_PaymentError()
		{
			// Arrange
			var paymentRequest = new Payment
			{
				Amount = 10,
				CardExpiryDate = DateTime.Now,
				CardNumber = 3566000020000410,
				CurrencyCode = "GDP",
				CVC = 132,
				FullName = "John Doe",
				IsSuccessful = false,
				PaymentId = Guid.NewGuid(),
				RequestDate = DateTime.Now,
				Status = PaymentStatus.RequestRecieved,
				Card = new Card
				{
					BankName = "TestBank",
					CardNumber = 3566000020000410,
					CVC = 132,
					ExpiryDate = DateTime.Now,
					Id = Guid.NewGuid()
				},
				User = new User
				{
					Id = Guid.NewGuid(),
					DateOfBirth = DateTime.Now.AddYears(-22),
					Fullname = "John Doe"
				},
				RecievingBankName = "TestBank"
			};

			// Act
			var paymentResponse = Service.ProcessPayment(
				paymentRequest);

			// Assert
			Assert.IsNotNull(paymentResponse);
			Assert.IsFalse(paymentResponse.IsSuccessful);
			Assert.AreEqual(PaymentStatus.Error, paymentResponse.Status);
			Assert.AreEqual("Internal Error: Please try again later or contact support.", paymentResponse.Message);

		}

		#endregion

		#region GetPaymentTests

		[Test, Category("Service Get Payment Tests")]
		public void GetPayment_FindPaymentFromId_PaymentFound()
		{
			// Arrange
			var paymentId = Guid.Parse("e4196f31-3019-456d-96d7-af6d6cc84be7");

			// Act
			var paymentResponse = Service.GetPayment(
				paymentId);

			// Assert
			Assert.IsNotNull(paymentResponse);
			Assert.IsTrue(paymentResponse.IsSuccessful);
			Assert.AreEqual(PaymentStatus.RequestSucceded, paymentResponse.Status);

		}

		[Test, Category("Service Get Payment Tests")]
		public void GetPayment_FindPaymentFromId_PaymentNotFound()
		{
			// Arrange
			var paymentId = Guid.Parse("21e91399-40f7-41a4-992f-429effe7a1b8");

			// Act
			var paymentResponse = Service.GetPayment(
				paymentId);

			// Assert
			Assert.IsNotNull(paymentResponse);
			Assert.IsFalse(paymentResponse.IsSuccessful);
			Assert.AreEqual(PaymentStatus.Error, paymentResponse.Status);

		}

		#endregion

		#region Properties 

		private MockRepository MockRepository { get; set; }

		private static IContainer Container { get; set; }

		private IPaymentService Service { get; set; }

		private Mock<ILogger<PaymentService>> MockLogger { get; set; }
		private Mock<IBankEndpoint> MockBankEndpoint { get; set; }
		private Mock<IPaymentRepo> MockPaymentRepo { get; set; }

		#endregion
	}
}

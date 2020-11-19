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

namespace PaymentGatewayServiceTests
{
	[TestFixture]
	public class PaymentServiceTests
	{
		#region Setup

		[SetUp]
		public void SetUp()
		{
			var builder = new ContainerBuilder();

			builder.RegisterType<TestBankEndpoint>().AsImplementedInterfaces().Keyed<IBankEndpoint>("TestBank");
			builder.RegisterType<AlternateTestBankEndpoint>().AsImplementedInterfaces().Keyed<IBankEndpoint>("AlternateBank");

			builder.Build();

			MockRepository = new MockRepository(MockBehavior.Strict);

			MockLogger = MockRepository.Create<ILogger<PaymentService>>(MockBehavior.Loose);
			MockBankEndpoint = MockRepository.Create<IIndex<string, IBankEndpoint>>();
			MockPaymentRepo = MockRepository.Create<IPaymentRepo>(MockBehavior.Loose);


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

			//MockBankEndpoint.Setup(m => m.SendPayment(
			//	It.Is<Payment>(payment => payment.CardNumber == 374245455400126)))
			//	.Returns<Payment>(payment => payment = new Payment
			//	{
			//		Status = PaymentStatus.InsuffucentFunds,
			//		IsSuccessful = false,
			//		Message = "Card Declined: Insuffucent Funds"
			//	});
			//MockBankEndpoint.Setup(m => m.SendPayment(
			//	It.Is<Payment>(payment => payment.CardNumber == 378282246310005)))
			//	.Returns<Payment>(payment => payment = new Payment
			//	{
			//		Status = PaymentStatus.CardNotActivated,
			//		IsSuccessful = false,
			//		Message = "Card Declined: Card Not Activated"
			//	});
			//MockBankEndpoint.Setup(m => m.SendPayment(
			//	It.Is<Payment>(payment => payment.CardNumber == 6250941006528599)))
			//	.Returns<Payment>(payment => payment = new Payment
			//	{
			//		Status = PaymentStatus.StolenCancelled,
			//		IsSuccessful = false,
			//		Message = "Card Declined: Stolen/Cancelled"
			//	});
			//MockBankEndpoint.Setup(m => m.SendPayment(
			//	It.Is<Payment>(payment => payment.CardNumber == 60115564485789458)))
			//	.Returns<Payment>(payment => payment = new Payment
			//	{
			//		Status = PaymentStatus.InvalidCardCredentials,
			//		IsSuccessful = false,
			//		Message = "Card Declined: Invalid Card Credentials"
			//	});
			//MockBankEndpoint.Setup(m => m.SendPayment(
			//	It.Is<Payment>(payment => payment.CardNumber == 6011000991300009)))
			//	.Returns<Payment>(payment => payment = new Payment
			//	{
			//		Status = PaymentStatus.CardExpired,
			//		IsSuccessful = false,
			//		Message = "Card Declined: Card Expired"
			//	});
			//MockBankEndpoint.Setup(m => m.SendPayment(
			//	It.Is<Payment>(payment => payment.CardNumber == 3566000020000410)))
			//	.Returns<Payment>(payment => payment = new Payment
			//	{
			//		Status = PaymentStatus.Error,
			//		IsSuccessful = false,
			//		Message = "Internal Error: Please try again later or contact support."
			//	});
			//MockBankEndpoint.Setup(m => m.SendPayment(
			//	It.Is<Payment>(payment => payment.CardNumber == 5425233430109903)))
			//	.Returns<Payment>(payment => payment = new Payment
			//	{
			//		Status = PaymentStatus.RequestSucceded,
			//		IsSuccessful = true,
			//		Message = string.Empty
			//	});

			#endregion

			///////////////////////////////////
			///////////////////////////////////
			///////////////////////////////////
			///////////////////////////////////
			MockRepository = new MockRepository(MockBehavior.Strict) { DefaultValue = DefaultValue.Empty };
		}

		private PaymentService CreateService()
		{
			return new PaymentService(
					MockLogger.Object,
					MockPaymentRepo.Object,
					MockBankEndpoint.Object);
		}

		#endregion

		#region StorePaymentTests

		[Test]
		public void ProcessPayment_PaymentSentToBank_PaymentSuccess()
		{
			// Arrange
			var service = this.CreateService();
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
			var paymentResponse = service.ProcessPayment(
				paymentRequest);

			// Assert
			Assert.IsNotNull(paymentResponse);
			Assert.IsTrue(paymentResponse.IsSuccessful);
			Assert.AreEqual(PaymentStatus.RequestSucceded, paymentResponse.Status);
			MockRepository.VerifyAll();
		}

		[Test]
		public void ProcessPayment_PaymentSentToBank_InsufficantFunds()
		{
			// Arrange
			var service = this.CreateService();
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
			var paymentResponse = service.ProcessPayment(
				paymentRequest);

			// Assert
			Assert.IsNotNull(paymentResponse);
			Assert.IsFalse(paymentResponse.IsSuccessful);
			Assert.AreEqual(PaymentStatus.InsuffucentFunds, paymentResponse.Status);
			Assert.AreEqual("Card Declined: Insuffucent Funds", paymentResponse.Message);
			MockRepository.VerifyAll();
		}

		[Test]
		public void ProcessPayment_PaymentSentToBank_CardNotActive()
		{
			// Arrange
			var service = this.CreateService();
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
			var paymentResponse = service.ProcessPayment(
				paymentRequest);

			// Assert
			Assert.IsNotNull(paymentResponse);
			Assert.IsFalse(paymentResponse.IsSuccessful);
			Assert.AreEqual(PaymentStatus.CardNotActivated, paymentResponse.Status);
			Assert.AreEqual("Card Declined: Card Not Activated", paymentResponse.Message);
			MockRepository.VerifyAll();
		}

		[Test]
		public void ProcessPayment_PaymentSentToBank_CardStolenCancelled()
		{
			// Arrange
			var service = this.CreateService();
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
			var paymentResponse = service.ProcessPayment(
				paymentRequest);

			// Assert
			Assert.IsNotNull(paymentResponse);
			Assert.IsFalse(paymentResponse.IsSuccessful);
			Assert.AreEqual(PaymentStatus.StolenCancelled, paymentResponse.Status);
			Assert.AreEqual("Card Declined: Stolen/Cancelled", paymentResponse.Message);
			MockRepository.VerifyAll();
		}

		[Test]
		public void ProcessPayment_PaymentSentToBank_InvalidCardCredentials()
		{
			// Arrange
			var service = this.CreateService();
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
			var paymentResponse = service.ProcessPayment(
				paymentRequest);

			// Assert
			Assert.IsNotNull(paymentResponse);
			Assert.IsFalse(paymentResponse.IsSuccessful);
			Assert.AreEqual(PaymentStatus.InvalidCardCredentials, paymentResponse.Status);
			Assert.AreEqual("Card Declined: Invalid Card Credentials", paymentResponse.Message);
			MockRepository.VerifyAll();
		}

		[Test]
		public void ProcessPayment_PaymentSentToBank_CardExpired()
		{
			// Arrange
			var service = this.CreateService();
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
			var paymentResponse = service.ProcessPayment(
				paymentRequest);

			// Assert
			Assert.IsNotNull(paymentResponse);
			Assert.IsFalse(paymentResponse.IsSuccessful);
			Assert.AreEqual(PaymentStatus.CardExpired, paymentResponse.Status);
			Assert.AreEqual("Card Declined: Card Expired", paymentResponse.Message);
			MockRepository.VerifyAll();
		}

		[Test]
		public void ProcessPayment_PaymentSentToBank_PaymentError()
		{
			// Arrange
			var service = this.CreateService();
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
			var paymentResponse = service.ProcessPayment(
				paymentRequest);

			// Assert
			Assert.IsNotNull(paymentResponse);
			Assert.IsFalse(paymentResponse.IsSuccessful);
			Assert.AreEqual(PaymentStatus.Error, paymentResponse.Status);
			Assert.AreEqual("Internal Error: Please try again later or contact support.", paymentResponse.Message);
			MockRepository.VerifyAll();
		}

		#endregion

		#region StorePaymentTests

		[Test]
		public void GetPayment_FindPaymentFromId_PaymentFound()
		{
			// Arrange
			var service = this.CreateService();
			var paymentId = Guid.Parse("e4196f31-3019-456d-96d7-af6d6cc84be7");

			// Act
			var paymentResponse = service.GetPayment(
				paymentId);

			// Assert
			Assert.IsNotNull(paymentResponse);
			Assert.IsTrue(paymentResponse.IsSuccessful);
			Assert.AreEqual(PaymentStatus.RequestSucceded, paymentResponse.Status);
			MockRepository.VerifyAll();
		}

		[Test]
		public void GetPayment_FindPaymentFromId_PaymentNotFound()
		{
			// Arrange
			var service = this.CreateService();
			var paymentId = Guid.Parse("21e91399-40f7-41a4-992f-429effe7a1b8");

			// Act
			var paymentResponse = service.GetPayment(
				paymentId);

			// Assert
			Assert.IsNotNull(paymentResponse);
			Assert.IsFalse(paymentResponse.IsSuccessful);
			Assert.AreEqual(PaymentStatus.Error, paymentResponse.Status);
			MockRepository.VerifyAll();
		}

		#endregion

		#region Properties 

		private MockRepository MockRepository { get; set; }

		private Mock<ILogger<PaymentService>> MockLogger { get; set; }
		private Mock<IIndex<string, IBankEndpoint>> MockBankEndpoint { get; set; }
		private Mock<IBankEndpoint> MockTestBankEndpoint { get; set; }
		private Mock<IBankEndpoint> MockAlternateBankEndpoint { get; set; }
		private Mock<IPaymentRepo> MockPaymentRepo { get; set; }

		#endregion
	}
}

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

namespace PaymentGatewayServiceTests
{
	[TestFixture]
	public class PaymentServiceTests
	{
		#region Setup

		[SetUp]
		public void SetUp()
		{
			MockRepository = new MockRepository(MockBehavior.Strict);

			MockLogger = MockRepository.Create<ILogger>(MockBehavior.Loose);
			MockBankEndpoint = MockRepository.Create<IBankEndpoint>();
			MockPaymentRepo = MockRepository.Create<IPaymentRepo>();

			MockBankEndpoint.Setup(m => m.SendPayment(
				It.Is<Payment>(payment => payment.CardNumber == 374245455400126)))
				.Returns<Payment>(payment => payment = new Payment
				{
					Status = PaymentStatus.RequestFailed,
					IsSuccessful = false,
					Message = "Card Declined: Insuffucent Funds"
				});
			MockBankEndpoint.Setup(m => m.SendPayment(
				It.Is<Payment>(payment => payment.CardNumber == 378282246310005)))
				.Returns<Payment>(payment => payment = new Payment
				{
					Status = PaymentStatus.RequestFailed,
					IsSuccessful = false,
					Message = "Card Declined: Card Not Activated"
				});
			MockBankEndpoint.Setup(m => m.SendPayment(
				It.Is<Payment>(payment => payment.CardNumber == 6250941006528599)))
				.Returns<Payment>(payment => payment = new Payment
				{
					Status = PaymentStatus.RequestFailed,
					IsSuccessful = false,
					Message = "Card Declined: Stolen/Cancelled"
				});
			MockBankEndpoint.Setup(m => m.SendPayment(
				It.Is<Payment>(payment => payment.CardNumber == 60115564485789458)))
				.Returns<Payment>(payment => payment = new Payment
				{
					Status = PaymentStatus.RequestFailed,
					IsSuccessful = false,
					Message = "Card Declined: Invalid Card Credentials"
				});
			MockBankEndpoint.Setup(m => m.SendPayment(
				It.Is<Payment>(payment => payment.CardNumber == 6011000991300009)))
				.Returns<Payment>(payment => payment = new Payment
				{
					Status = PaymentStatus.RequestFailed,
					IsSuccessful = false,
					Message = "Card Declined: Card Expired"
				});
			MockBankEndpoint.Setup(m => m.SendPayment(
				It.Is<Payment>(payment => payment.CardNumber == 3566000020000410)))
				.Returns<Payment>(payment => payment = new Payment
				{
					Status = PaymentStatus.RequestFailed,
					IsSuccessful = false,
					Message = "Internal Error: Please try again later or contact support."
				});
			MockBankEndpoint.Setup(m => m.SendPayment(
				It.Is<Payment>(payment => payment.CardNumber == 5425233430109903)))
				.Returns<Payment>(payment => payment = new Payment
				{
					Status = PaymentStatus.RequestSucceded,
					IsSuccessful = true,
					Message = string.Empty
				});

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

		#region Tests

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
				Status = PaymentStatus.RequestRecieved
			};

			// Act
			var paymentResponse = service.ProcessPayment(
				paymentRequest);

			// Assert
			Assert.IsNotNull(paymentResponse);
			Assert.IsTrue(paymentResponse.IsSuccessful);
			Assert.AreEqual(paymentResponse.Status, PaymentStatus.RequestSucceded);
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
				Status = PaymentStatus.RequestRecieved
			};

			// Act
			var paymentResponse = service.ProcessPayment(
				paymentRequest);

			// Assert
			Assert.IsNotNull(paymentResponse);
			Assert.IsFalse(paymentResponse.IsSuccessful);
			Assert.AreEqual(paymentResponse.Status, PaymentStatus.RequestFailed);
			Assert.AreEqual(paymentResponse.Message, "Card Declined: Insuffucent Funds");
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
				Status = PaymentStatus.RequestRecieved
			};

			// Act
			var paymentResponse = service.ProcessPayment(
				paymentRequest);

			// Assert
			Assert.IsNotNull(paymentResponse);
			Assert.IsFalse(paymentResponse.IsSuccessful);
			Assert.AreEqual(paymentResponse.Status, PaymentStatus.RequestFailed);
			Assert.AreEqual(paymentResponse.Message, "Card Declined: Card Not Activated");
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
				Status = PaymentStatus.RequestRecieved
			};

			// Act
			var paymentResponse = service.ProcessPayment(
				paymentRequest);

			// Assert
			Assert.IsNotNull(paymentResponse);
			Assert.IsFalse(paymentResponse.IsSuccessful);
			Assert.AreEqual(paymentResponse.Status, PaymentStatus.RequestFailed);
			Assert.AreEqual(paymentResponse.Message, "Card Declined: Stolen/Cancelled");
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
				Status = PaymentStatus.RequestRecieved
			};

			// Act
			var paymentResponse = service.ProcessPayment(
				paymentRequest);

			// Assert
			Assert.IsNotNull(paymentResponse);
			Assert.IsFalse(paymentResponse.IsSuccessful);
			Assert.AreEqual(paymentResponse.Status, PaymentStatus.RequestFailed);
			Assert.AreEqual(paymentResponse.Message, "Card Declined: Invalid Card Credentials");
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
				Status = PaymentStatus.RequestRecieved
			};

			// Act
			var paymentResponse = service.ProcessPayment(
				paymentRequest);

			// Assert
			Assert.IsNotNull(paymentResponse);
			Assert.IsFalse(paymentResponse.IsSuccessful);
			Assert.AreEqual(paymentResponse.Status, PaymentStatus.RequestFailed);
			Assert.AreEqual(paymentResponse.Message, "Card Declined: Card Expired");
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
				Status = PaymentStatus.RequestRecieved
			};

			// Act
			var paymentResponse = service.ProcessPayment(
				paymentRequest);

			// Assert
			Assert.IsNotNull(paymentResponse);
			Assert.IsFalse(paymentResponse.IsSuccessful);
			Assert.AreEqual(paymentResponse.Status, PaymentStatus.RequestFailed);
			Assert.AreEqual(paymentResponse.Message, "Internal Error: Please try again later or contact support.");
			MockRepository.VerifyAll();
		}

		#endregion

		#region Properties 

		private MockRepository MockRepository { get; set; }

		private Mock<ILogger> MockLogger { get; set; }
		private Mock<IBankEndpoint> MockBankEndpoint { get; set; }
		private Mock<IPaymentRepo> MockPaymentRepo { get; set; }

		#endregion
	}
}

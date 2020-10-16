using AcquiringBankMock.Interfaces;
using AutoMapper;
using Common.Models;
using Moq;
using NUnit.Framework;
using PaymentGatewayService;
using Serilog;
using System;

namespace PaymentGatewayServiceTests
{
	[TestFixture]
	public class PaymentServiceTests
	{
		private MockRepository mockRepository;

		private Mock<ILogger> mockLogger;
		private Mock<IMapper> mockMapper;
		private Mock<IPaymentController> mockBankApi;

		[SetUp]
		public void SetUp()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);

			this.mockLogger = this.mockRepository.Create<ILogger>();
			this.mockMapper = this.mockRepository.Create<IMapper>();
			this.mockBankApi = this.mockRepository.Create<IPaymentController>();
		}

		private PaymentService CreateService()
		{
			return new PaymentService(
					this.mockLogger.Object,
					this.mockMapper.Object,
					this.mockBankApi.Object);
		}

		[Test]
		public void ProcessPayment_PaymentSentToBank_PaymentSuccess()
		{
			// Arrange
			var service = this.CreateService();
			Payment paymentRequest = null;

			// Act
			var result = service.ProcessPayment(
				paymentRequest);

			// Assert
			Assert.IsTrue(result);
			this.mockRepository.VerifyAll();
		}
	}
}

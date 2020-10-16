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

		[SetUp]
		public void SetUp()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);

			this.mockLogger = this.mockRepository.Create<ILogger>();
			this.mockMapper = this.mockRepository.Create<IMapper>();
		}

		private PaymentService CreateService()
		{
			return new PaymentService(
					this.mockLogger.Object,
					this.mockMapper.Object);
		}

		[Test]
		public void ProcessPayment_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var service = this.CreateService();
			Payment paymentRequest = null;

			// Act
			var result = service.ProcessPayment(
				paymentRequest);

			// Assert
			Assert.Fail();
			this.mockRepository.VerifyAll();
		}
	}
}

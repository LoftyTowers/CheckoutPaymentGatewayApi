using CheckoutPaymentGateway.Controllers;
using CheckoutPaymentGateway.Models;
using Moq;
using NUnit.Framework;
using System;

namespace CheckoutPaymentGatewayTests.Controllers
{
	[TestFixture]
	public class PaymentControllerTests
	{
		private MockRepository mockRepository;



		[SetUp]
		public void SetUp()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);


		}

		//private PaymentController CreatePaymentController()
		//{
		//	return new PaymentController();
		//}

		[Test]
		public void CreatePayment_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			//var paymentController = this.CreatePaymentController();
			//PaymentRequest body = null;

			//// Act
			//var result = paymentController.CreatePayment(
			//	body);

			//// Assert
			//Assert.Fail();
			//this.mockRepository.VerifyAll();
		}
	}
}

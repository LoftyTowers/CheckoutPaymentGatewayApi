using AutoMapper;
using CheckoutPaymentGateway.Controllers;
using CheckoutPaymentGateway.Models;
using Common.Enums;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PaymentGatewayService.Interfaces;
using System;

namespace CheckoutPaymentGatewayTests.Controllers
{
	[TestFixture]
	public class PaymentControllerTests
	{
		private MockRepository mockRepository;

		private Mock<ILogger<PaymentController>> mockLogger;
		private Mock<IMapper> mockMapper;
		private Mock<IPaymentService> mockPaymentService;

		[SetUp]
		public void SetUp()
		{
			this.mockRepository = new MockRepository(MockBehavior.Strict);

			this.mockLogger = this.mockRepository.Create<ILogger<PaymentController>>(MockBehavior.Loose);
			this.mockMapper = this.mockRepository.Create<IMapper>();
			this.mockPaymentService = this.mockRepository.Create<IPaymentService>();

			mockPaymentService.Setup(m => m.GetPayment(It.Is<Guid>(i => i == new Guid("af188a89-0d2a-4863-a596-f7844364ac09"))))
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
			mockPaymentService.Setup(m => m.GetPayment(It.Is<Guid>(i => i == new Guid("14077807-b564-4b0b-9e7a-6b8dbd26e615"))))
				.Returns((Guid paymentId) => new Payment
				{
					PaymentId = new Guid("14077807-b564-4b0b-9e7a-6b8dbd26e615"),
					Status = PaymentStatus.RequestDoesNotExist,
					Message = $"The payment: 14077807-b564-4b0b-9e7a-6b8dbd26e615 does not exist"
				});
			mockPaymentService.Setup(m => m.GetPayment(It.Is<Guid>(i => i == new Guid("13b702ef-18be-427e-931d-21b866aed500"))))
				.Returns((Guid paymentId) => new Payment
				{
					Status = PaymentStatus.Error
				});
			mockPaymentService.Setup(m => m.GetPayment(It.Is<Guid>(i => i == new Guid("682f6880-5415-44a9-a7bf-c8d1f9a6f43e"))))
				.Returns((Guid paymentId) => new Payment
				{
					Status = PaymentStatus.Unknown
				});

		}

		private PaymentController CreatePaymentController()
		{
			return new PaymentController(
					this.mockLogger.Object,
					this.mockMapper.Object,
					this.mockPaymentService.Object);
		}

		[Test, Category("Controller Echo Tests")]
		public void Echo_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var paymentController = this.CreatePaymentController();
			string echo = "It is a new world";

			// Act
			var result = paymentController.Echo(echo).Result as OkObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Value);
			Assert.AreEqual("It is a new world", result.Value);
			this.mockRepository.VerifyAll();
		}

		[Test, Category("Controller Send Payment Tests")]
		public void CreatePayment_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var paymentController = this.CreatePaymentController();
			PaymentRequest body = null;

			// Act
			var result = paymentController.CreatePayment(body).Result as OkObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(10, result.StatusCode);
			Assert.IsNotNull(result.Value);
			this.mockRepository.VerifyAll();
		}

		[Test, Category("Controller Get Payment Tests")]
		public void GetPayment_CanGetPayment_OkObjectResult()
		{
			// Arrange
			var paymentController = this.CreatePaymentController();
			Guid body = new Guid("af188a89-0d2a-4863-a596-f7844364ac09");

			// Act
			var result = paymentController.GetPayment(body).Result as OkObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Value);
			Assert.IsInstanceOf<PaymentResponse>(result.Value, "response is CheckoutPaymentGatewayModelsPaymentResponse");

			var response = result.Value as PaymentResponse;

			Assert.IsNotNull(response.Amount);
			Assert.IsNotNull(response.CurrencyCode);
			Assert.IsNotNull(response.FullName);
			Assert.IsNotNull(response.Id);
			Assert.IsNotNull(response.IsSuccessful);
			Assert.IsNotNull(response.RequestDate);

			this.mockRepository.VerifyAll();
		}

		[Test, Category("Controller Get Payment Tests")]
		public void GetPayment_CanGetPayment_NotFoundObjectResult()
		{
			// Arrange
			var paymentController = this.CreatePaymentController();
			Guid body = new Guid("14077807-b564-4b0b-9e7a-6b8dbd26e615");

			// Act
			var result = paymentController.GetPayment(body).Result as NotFoundObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Value);
			Assert.IsInstanceOf<PaymentResponse>(result.Value, "response is CheckoutPaymentGatewayModelsPaymentResponse");

			var response = result.Value as PaymentResponse;

			Assert.IsNotNull(response.Amount);
			Assert.IsNotNull(response.CurrencyCode);
			Assert.IsNotNull(response.FullName);
			Assert.IsNotNull(response.Id);
			Assert.IsNotNull(response.IsSuccessful);
			Assert.IsNotNull(response.RequestDate);
			Assert.IsNotNull(response.IsSuccessful);

			this.mockRepository.VerifyAll();
		}

		[Test, Category("Controller Get Payment Tests")]
		public void GetPayment_CanGetPayment_Uknown500Error()
		{
			// Arrange
			var paymentController = this.CreatePaymentController();
			Guid body = new Guid("13b702ef-18be-427e-931d-21b866aed500");

			// Act
			var result = paymentController.GetPayment(body).Result as ObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Value);
			Assert.IsInstanceOf<PaymentResponse>(result.Value, "response is CheckoutPaymentGatewayModelsPaymentResponse");

			var response = result.Value as PaymentResponse;

			Assert.IsNotNull(response.Amount);
			Assert.IsNotNull(response.CurrencyCode);
			Assert.IsNotNull(response.FullName);
			Assert.IsNotNull(response.Id);
			Assert.IsNotNull(response.IsSuccessful);
			Assert.IsNotNull(response.RequestDate);
			Assert.IsNotNull(response.IsSuccessful);

			this.mockRepository.VerifyAll();
		}

		[Test, Category("Controller Get Payment Tests")]
		public void GetPayment_CanGetPayment_BadRequestObjectResult()
		{
			// Arrange
			var paymentController = this.CreatePaymentController();
			Guid body = new Guid("682f6880-5415-44a9-a7bf-c8d1f9a6f43e");

			// Act
			var result = paymentController.GetPayment(body).Result as BadRequestObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Value);
			Assert.IsInstanceOf<PaymentResponse>(result.Value, "response is CheckoutPaymentGatewayModelsPaymentResponse");

			var response = result.Value as PaymentResponse;

			Assert.IsNotNull(response.Amount);
			Assert.IsNotNull(response.CurrencyCode);
			Assert.IsNotNull(response.FullName);
			Assert.IsNotNull(response.Id);
			Assert.IsNotNull(response.IsSuccessful);
			Assert.IsNotNull(response.RequestDate);
			Assert.IsNotNull(response.IsSuccessful);

			this.mockRepository.VerifyAll();
		}
	}
}

using Autofac;
using AutoMapper;
using CheckoutPaymentGateway.Controllers;
using CheckoutPaymentGateway.Interfaces;
using CheckoutPaymentGateway.Models;
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

		[SetUp]
		public void SetUp()
		{
			MockRepository = new MockRepository(MockBehavior.Strict);

			MockLogger = MockRepository.Create<ILogger<PaymentController>>(MockBehavior.Loose);
			MockPaymentService = MockRepository.Create<IPaymentService>();

			#region MockPaymentService

			// Process Payment Mocks
			MockPaymentService.Setup(m => m.ProcessPayment(It.Is<Payment>(i => i.PaymentId == new Guid("af188a89-0d2a-4863-a596-f7844364ac09"))))
				.Returns((Payment paymentRequest) => new Payment
				{
					Amount = 10,
					CardExpiryDate = DateTime.Now,
					CardNumber = "5425233430109903",
					CurrencyCode = "GDP",
					CVC = 132,
					FullName = "John Doe",
					PaymentId = new Guid("af188a89-0d2a-4863-a596-f7844364ac09"),
					RequestDate = DateTime.Now,
					Status = Common.Enums.PaymentStatus.RequestSucceded,
					RecievingBankName = "TestBank",
					IsSuccessful = true
				});
			MockPaymentService.Setup(m => m.ProcessPayment(It.Is<Payment>(i => i.PaymentId == new Guid("ff58f525-5528-46e2-8b68-aacec43cd4c1"))))
				.Returns((Payment paymentRequest) => new Payment
				{
					Amount = 10,
					CardExpiryDate = DateTime.Now,
					CardNumber = "5425233430109903",
					CurrencyCode = "GDP",
					CVC = 132,
					FullName = "John Doe",
					PaymentId = new Guid("ff58f525-5528-46e2-8b68-aacec43cd4c1"),
					RequestDate = DateTime.Now,
					Status = Common.Enums.PaymentStatus.DuplicateRequest,
					RecievingBankName = "TestBank",
					IsSuccessful = false,
					Message = "Internal Error Please try again later"
				});
			MockPaymentService.Setup(m => m.ProcessPayment(It.Is<Payment>(i => i.PaymentId == new Guid("5cf9b529-5c88-4b35-accd-dfcafe96e180"))))
				.Returns((Payment paymentRequest) => new Payment
				{
					Amount = 10,
					CardExpiryDate = DateTime.Now,
					CardNumber = "5425233430109903",
					CurrencyCode = "GDP",
					CVC = 132,
					FullName = "John Doe",
					PaymentId = new Guid("5cf9b529-5c88-4b35-accd-dfcafe96e180"),
					RequestDate = DateTime.Now,
					Status = Common.Enums.PaymentStatus.Error,
					RecievingBankName = "TestBank",
					IsSuccessful = false,
					Message = "Internal Error Please try again later"
				});
			MockPaymentService.Setup(m => m.ProcessPayment(It.Is<Payment>(i => i.PaymentId == new Guid("bf2887a9-9eaf-4f75-a38a-e2d27b885c88"))))
				.Returns((Payment paymentRequest) => new Payment
				{
					Amount = 10,
					CardExpiryDate = DateTime.Now,
					CardNumber = "5425233430109903",
					CurrencyCode = "GDP",
					CVC = 132,
					FullName = "John Doe",
					PaymentId = new Guid("bf2887a9-9eaf-4f75-a38a-e2d27b885c88"),
					RequestDate = DateTime.Now,
					Status = Common.Enums.PaymentStatus.CardNotActivated,
					RecievingBankName = "TestBank",
					IsSuccessful = false,
					Message = "The customers card is not activated"
				});

			// Get Payment Mocks
			MockPaymentService.Setup(m => m.GetPayment(It.Is<Guid>(i => i == new Guid("af188a89-0d2a-4863-a596-f7844364ac09"))))
				.Returns((Guid paymentId) => new Payment
				{
					Amount = 10,
					CardExpiryDate = DateTime.Now,
					CardNumber = "5425233430109903",
					CurrencyCode = "GDP",
					CVC = 132,
					FullName = "John Doe",
					IsSuccessful = true,
					PaymentId = paymentId,
					RequestDate = DateTime.Now.AddDays(-1),
					Status = Common.Enums.PaymentStatus.RequestSucceded,
					RecievingBankName = "TestBank"
				});
			MockPaymentService.Setup(m => m.GetPayment(It.Is<Guid>(i => i == new Guid("14077807-b564-4b0b-9e7a-6b8dbd26e615"))))
				.Returns((Guid paymentId) => new Payment
				{
					PaymentId = new Guid("14077807-b564-4b0b-9e7a-6b8dbd26e615"),
					Status = Common.Enums.PaymentStatus.RequestDoesNotExist,
					Message = $"The payment: 14077807-b564-4b0b-9e7a-6b8dbd26e615 does not exist"
				});
			MockPaymentService.Setup(m => m.GetPayment(It.Is<Guid>(i => i == new Guid("13b702ef-18be-427e-931d-21b866aed500"))))
				.Returns((Guid paymentId) => new Payment
				{
					Status = Common.Enums.PaymentStatus.Error
				});
			MockPaymentService.Setup(m => m.GetPayment(It.Is<Guid>(i => i == new Guid("682f6880-5415-44a9-a7bf-c8d1f9a6f43e"))))
				.Returns((Guid paymentId) => new Payment
				{
					Status = Common.Enums.PaymentStatus.Unknown
				});

			#endregion


			var builder = new ContainerBuilder();

			builder.Register(context => new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<PaymentRequest, Payment>()
					.ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.Id));
				cfg.CreateMap<Payment, PaymentResponse>()
					.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PaymentId));
				cfg.CreateMap<PaymentRequest, User>()
					.ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
					.ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => src.FullName))
					.ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<PaymentRequest, Card>()
					.ForMember(dest => dest.CardNumber, opt => opt.MapFrom(src => src.CardNumber))
					.ForMember(dest => dest.ExpiryDate, opt => opt.MapFrom(src => src.CardExpiryDate))
					.ForMember(dest => dest.CVC, opt => opt.MapFrom(src => src.CVC))
					.ForMember(dest => dest.BankName, opt => opt.MapFrom(src => src.SendingBankName))
					.ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<User, PaymentResponse>();
				cfg.CreateMap<Card, PaymentResponse>();
			})).AsSelf().SingleInstance();

			builder.Register(c =>
			{
				var context = c.Resolve<IComponentContext>();
				var config = context.Resolve<MapperConfiguration>();
				return config.CreateMapper(context.Resolve);
			}).As<IMapper>().InstancePerLifetimeScope();


			builder.RegisterInstance(MockLogger.Object).AsImplementedInterfaces();
			builder.RegisterInstance(MockPaymentService.Object).AsImplementedInterfaces();
			builder.RegisterType<PaymentController>().AsImplementedInterfaces();

			Container = builder.Build();

			PaymentController = Container.Resolve<IPaymentController>();
		}


		[Test, Category("Controller Echo Tests")]
		[Ignore("Temporarily ignored until bearer tokens are added to the test calls")]
		public void Echo_ApiIsREachable_ReflectsInputString()
		{
			// Arrange
			string echo = "It is a new world";

			// Act
			var result = PaymentController.Echo(echo).Result as OkObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Value);
			Assert.AreEqual("It is a new world", result.Value);
		}

		[Test, Category("Controller Send Payment Tests")]
		[Ignore("Temporarily ignored until bearer tokens are added to the test calls")]
		public void CreatePayment_CanCreatePayment_OkObjectResult()
		{
			// Arrange
			var body = new PaymentRequest
			{
				Amount = 10,
				CardExpiryDate = DateTime.Now,
				CardNumber = "5425233430109903",
				CurrencyCode = "GDP",
				CVC = 132,
				FullName = "John Doe",
				Id = new Guid("af188a89-0d2a-4863-a596-f7844364ac09"),
				RequestDate = DateTime.Now,
				DateOfBirth = DateTime.Now.AddYears(-22),
				RecievingBankName = "TestBank"
			};

			// Act
			var result = PaymentController.CreatePayment(body).Result as OkObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Value);

			Assert.IsInstanceOf<PaymentResponse>(result.Value, "response is CheckoutPaymentGatewayModelsPaymentResponse");
		}

		[Test, Category("Controller Send Payment Tests")]
		[Ignore("Temporarily ignored until bearer tokens are added to the test calls")]
		public void CreatePayment_CanCreatePayment_ConflictObjectResult()
		{
			// Arrange
			var body = new PaymentRequest
			{
				Amount = 10,
				CardExpiryDate = DateTime.Now,
				CardNumber = "5425233430109903",
				CurrencyCode = "GDP",
				CVC = 132,
				FullName = "John Doe",
				Id = new Guid("ff58f525-5528-46e2-8b68-aacec43cd4c1"),
				RequestDate = DateTime.Now,
				DateOfBirth = DateTime.Now.AddYears(-22),
				RecievingBankName = "TestBank"
			};

			// Act
			var result = PaymentController.CreatePayment(body).Result as ConflictObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Value);

			Assert.IsInstanceOf<PaymentResponse>(result.Value, "response is CheckoutPaymentGatewayModelsPaymentResponse");
		}

		[Test, Category("Controller Send Payment Tests")]
		[Ignore("Temporarily ignored until bearer tokens are added to the test calls")]
		public void CreatePayment_CanCreatePayment_StatusCode500()
		{
			// Arrange
			var body = new PaymentRequest
			{
				Amount = 10,
				CardExpiryDate = DateTime.Now,
				CardNumber = "5425233430109903",
				CurrencyCode = "GDP",
				CVC = 132,
				FullName = "John Doe",
				Id = new Guid("5cf9b529-5c88-4b35-accd-dfcafe96e180"),
				RequestDate = DateTime.Now,
				DateOfBirth = DateTime.Now.AddYears(-22),
				RecievingBankName = "TestBank"
			};

			// Act
			var result = PaymentController.CreatePayment(body).Result as ObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(500, result.StatusCode);
			Assert.IsNotNull(result.Value);

			Assert.IsInstanceOf<PaymentResponse>(result.Value, "response is CheckoutPaymentGatewayModelsPaymentResponse");
		}

		[Test, Category("Controller Send Payment Tests")]
		[Ignore("Temporarily ignored until bearer tokens are added to the test calls")]
		public void CreatePayment_CanCreatePayment_BadRequestObjectResult()
		{
			// Arrange
			var body = new PaymentRequest
			{
				Amount = 10,
				CardExpiryDate = DateTime.Now,
				CardNumber = "5425233430109903",
				CurrencyCode = "GDP",
				CVC = 132,
				FullName = "John Doe",
				Id = new Guid("bf2887a9-9eaf-4f75-a38a-e2d27b885c88"),
				RequestDate = DateTime.Now,
				DateOfBirth = DateTime.Now.AddYears(-22),
				RecievingBankName = "TestBank"
			};

			// Act
			var result = PaymentController.CreatePayment(body).Result as BadRequestObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Value);

			Assert.IsInstanceOf<PaymentResponse>(result.Value, "response is CheckoutPaymentGatewayModelsPaymentResponse");
		}

		[Test, Category("Controller Get Payment Tests")]
		[Ignore("Temporarily ignored until bearer tokens are added to the test calls")]
		public void GetPayment_CanGetPayment_OkObjectResult()
		{
			// Arrange
			Guid body = new Guid("af188a89-0d2a-4863-a596-f7844364ac09");

			// Act
			var result = PaymentController.GetPayment(body).Result as OkObjectResult;

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
		}

		[Test, Category("Controller Get Payment Tests")]
		[Ignore("Temporarily ignored until bearer tokens are added to the test calls")]
		public void GetPayment_CanGetPayment_NotFoundObjectResult()
		{
			// Arrange
			Guid body = new Guid("14077807-b564-4b0b-9e7a-6b8dbd26e615");

			// Act
			var result = PaymentController.GetPayment(body).Result as NotFoundObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Value);
			Assert.IsInstanceOf<PaymentResponse>(result.Value, "response is CheckoutPaymentGatewayModelsPaymentResponse");

			var response = result.Value as PaymentResponse;

			Assert.IsNull(response.Amount);
			Assert.IsNull(response.CurrencyCode);
			Assert.IsNull(response.FullName);
			Assert.IsNull(response.RequestDate);

			Assert.IsNotNull(response.Id);
			Assert.IsNotNull(response.IsSuccessful);
			Assert.IsNotNull(response.Message);
		}

		[Test, Category("Controller Get Payment Tests")]
		[Ignore("Temporarily ignored until bearer tokens are added to the test calls")]
		public void GetPayment_CanGetPayment_Uknown500Error()
		{
			// Arrange
			Guid body = new Guid("13b702ef-18be-427e-931d-21b866aed500");

			// Act
			var result = PaymentController.GetPayment(body).Result as ObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Value);
			Assert.IsInstanceOf<PaymentResponse>(result.Value, "response is CheckoutPaymentGatewayModelsPaymentResponse");

			var response = result.Value as PaymentResponse;

			Assert.IsNull(response.Amount);
			Assert.IsNull(response.CurrencyCode);
			Assert.IsNull(response.FullName);
			Assert.IsNull(response.RequestDate);

			Assert.IsNotNull(response.Id);
			Assert.IsNotNull(response.IsSuccessful);
		}

		[Test, Category("Controller Get Payment Tests")]
		[Ignore("Temporarily ignored until bearer tokens are added to the test calls")]
		public void GetPayment_CanGetPayment_BadRequestObjectResult()
		{
			// Arrange
			Guid body = new Guid("682f6880-5415-44a9-a7bf-c8d1f9a6f43e");

			// Act
			var result = PaymentController.GetPayment(body).Result as BadRequestObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Value);
			Assert.IsInstanceOf<PaymentResponse>(result.Value, "response is CheckoutPaymentGatewayModelsPaymentResponse");

			var response = result.Value as PaymentResponse;

			Assert.IsNull(response.Amount);
			Assert.IsNull(response.CurrencyCode);
			Assert.IsNull(response.FullName);
			Assert.IsNull(response.RequestDate);

			Assert.IsNotNull(response.Id);
			Assert.IsNotNull(response.IsSuccessful);
		}

		#region Properties

		private MockRepository MockRepository { get; set; }

		private Mock<ILogger<PaymentController>> MockLogger { get; set; }
		private Mock<IPaymentService> MockPaymentService { get; set; }

		private static IContainer Container { get; set; }

		private IPaymentController PaymentController { get; set; }

		#endregion
	}
}

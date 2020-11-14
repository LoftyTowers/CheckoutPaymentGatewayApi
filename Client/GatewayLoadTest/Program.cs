using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PaymentGatewayAPIClient.Api;
using PaymentGatewayAPIClient.Interfaces;
using System;

namespace GatewayLoadTest
{
	class Program
	{
		static void Main(string[] args)
		{
			var serviceCollection = new ServiceCollection();
			ConfigureServices(serviceCollection);

			var serviceProvider = serviceCollection.BuildServiceProvider();

			var runner = serviceProvider.GetService<PaymentsSetup>();
			Log = serviceProvider.GetService<ILogger<Program>>();

			Console.WriteLine("Enter the number of payments to generate, then press enter");
			var totalPaymentsString = Console.ReadLine();
			int totalPayments = 10;

			if (!string.IsNullOrWhiteSpace(totalPaymentsString))
			{
				int.TryParse(totalPaymentsString, out totalPayments);
			}

			var payments = runner.GeneratePayments(totalPayments);

			var paymentResponses = runner.SendPayments(payments);

			var filePath = runner.StoreErrors(paymentResponses.Errors);

			Log.LogInformation($"Total Payments: {totalPayments}");
			Log.LogInformation($"Time taken to process all payments: {paymentResponses.TotalTime}");
			Log.LogInformation($"Successfull Payments: {paymentResponses.SuccessCount}");
			Log.LogInformation($"Failed Payments: {paymentResponses.FailedCount}");
			Log.LogInformation($"failed payment responses file path: {filePath}");
		}

		private static void ConfigureServices(IServiceCollection services)
		{
			services.AddLogging(configure => configure.AddConsole())
								 .AddTransient<PaymentsSetup>()
								 .AddTransient<IPaymentApi, PaymentApi>();
		}

		#region Properties

		public static ILogger<Program> Log { get; set; }

		#endregion
	}
}

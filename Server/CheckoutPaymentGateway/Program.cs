using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using CheckoutPaymentGateway;
using Serilog;
using System;

namespace CheckoutPaymentGateway
{
	/// <summary>
	/// Program
	/// </summary>
	public class Program
	{
		/// <summary>
		/// Main
		/// </summary>
		/// <param name="args"></param>
		public static void Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
						 .Enrich.FromLogContext()
						 .WriteTo.Console()
						 .WriteTo.Elasticsearch()
						 .MinimumLevel.Debug()
						 .CreateLogger();
			try
			{
				CreateHostBuilder(args).Build().Run();
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "Application start-up failed");
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

		/// <summary>
		/// Create the host builder.
		/// </summary>
		/// <param name="args"></param>
		/// <returns>IHostBuilder</returns>
		public static IHostBuilder CreateHostBuilder(string[] args) =>
						Host.CreateDefaultBuilder(args)
						.UseServiceProviderFactory(new AutofacServiceProviderFactory())
						.UseSerilog()
						.ConfigureWebHostDefaults(webHostBuilder =>
						{
							webHostBuilder
								.UseContentRoot(Directory.GetCurrentDirectory())
								.UseIISIntegration()
								.UseStartup<Startup>();
						});
	}
}

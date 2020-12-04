using Microsoft.AspNetCore.Hosting;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using Serilog;
using System;
using Serilog.Sinks.Elasticsearch;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Serilog.Exceptions;

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
			//configure logging first
			ConfigureLogging();
			try
			{
				CreateHostBuilder(args).Build().Run();
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, $"Failed to start {Assembly.GetExecutingAssembly().GetName().Name}");
				throw;
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}
		private static void ConfigureLogging()
		{
			var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
			var configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile(
					$"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
					optional: true)
				.Build();

			Log.Logger = new LoggerConfiguration()
				.Enrich.FromLogContext()
				.Enrich.WithExceptionDetails()
				.WriteTo.Debug()
				.WriteTo.Console()
				.WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
				.Enrich.WithProperty("Environment", environment)
				.ReadFrom.Configuration(configuration)
				.CreateLogger();
		}

		private static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
		{
			return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
			{
				AutoRegisterTemplate = true,
				IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
			};
		}

		/// <summary>
		/// Create the host builder.
		/// </summary>
		/// <param name="args"></param>
		/// <returns>IHostBuilder</returns>
		public static IHostBuilder CreateHostBuilder(string[] args) =>
						Host.CreateDefaultBuilder(args)
						.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureAppConfiguration(configuration =>
						{
							configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
							configuration.AddJsonFile(
								$"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
								optional: true);
						})
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

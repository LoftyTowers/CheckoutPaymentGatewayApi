/*
 * Payment Gateway API
 *
 * Validates payment requests, stores card information, forwards payment requests and accepts responses from the acquiring bank.
 *
 * OpenAPI spec version: v1
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using CheckoutPaymentGateway.Filters;
using Autofac;
using PaymentGatewayService;
using PaymentGatewayService.Interfaces;
using AutoMapper;
using Common.Models;
using CheckoutPaymentGateway.Models;
using AutofacSerilogIntegration;
using PaymentGatewayService.Services;
using PaymentGatewayService.BankEndpoints;
using Repositories.PaymentsDb.DbContexts;
using Microsoft.EntityFrameworkCore;
using Repositories.PaymentsDb.Repos;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace CheckoutPaymentGateway
{
	/// <summary>
	/// Startup
	/// </summary>
	public class Startup
	{
		private const string IssuerSigningKeyString = "WeAllLoveMrCrumbsHardWork";
		private readonly IWebHostEnvironment _hostingEnv;

		private IConfiguration Configuration { get; }

		private ILifetimeScope AutofacContainer { get; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="env"></param>
		/// <param name="configuration"></param>
		public Startup(IWebHostEnvironment env, IConfiguration configuration)
		{
			_hostingEnv = env;
			Configuration = configuration;

			// In ASP.NET Core 3.0 `env` will be an IWebHostEnvironment, not IHostingEnvironment.
			var builder = new ConfigurationBuilder()
					.SetBasePath(env.ContentRootPath)
					.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
					.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
					.AddEnvironmentVariables();
			this.Configuration = builder.Build();
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to add services to the container.
		/// </summary>
		/// <param name="services"></param>
		public void ConfigureServices(IServiceCollection services)
		{
			// Add framework services.

			services
					.AddMvc(options =>
					{
						options.InputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter>();
						options.OutputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonOutputFormatter>();
					})
					.AddNewtonsoftJson(opts =>
					{
						opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
						opts.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
					})
					.AddXmlSerializerFormatters();

			//services.Configure<TokenManagement>(Configuration.GetSection("tokenManagement"));
			//var token = Configuration.GetSection("tokenManagement").Get<TokenManagement>();
			//var secret = Encoding.ASCII.GetBytes(token.Secret);

			services.AddDbContext<PaymentsDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("PaymentsDbEntities")));

			services.AddAuthentication(options =>
			{
				//options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				//options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				//options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme
			})
			.AddJwtBearer(options =>
			{
				options.SaveToken = true;
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidAudience = "https://localhost:6001",
					ValidIssuer = "https://localhost:6001",
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IssuerSigningKeyString))
				};
			});

			services.AddAuthorization(options =>
			{
				options.AddPolicy("PeterPolicy",
				policy =>
				{
					policy.RequireClaim("Peter");
				});
			});

			#region BootstrapperForTokeIssuerToken

			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IssuerSigningKeyString));



			var authClaims = new[]
			{
				new Claim("Peter","Pumpy")
			};
			foreach (var audience in new[] { "https://localhost:6001" })
			{
				var token = new JwtSecurityToken(
							 audience: audience,
							 issuer: "https://localhost:6001",
							 expires: DateTime.Now.AddYears(3),
							 claims: authClaims,
							 signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
				);
				var tokenIssuer = new JwtSecurityTokenHandler().WriteToken(token);
			}

			#endregion

			services.AddSwaggerGen()
			.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "V1",
					Title = "Payment Gateway API",
					Description = "Payment Gateway API (ASP.NET Core 3.1)",
					Contact = new OpenApiContact()
					{
						Name = "Swagger Codegen Contributors",
						Url = new Uri("https://github.com/swagger-api/swagger-codegen"),
						Email = string.Empty
					}
				});
				c.CustomSchemaIds(type => type.FullName);
				c.IncludeXmlComments($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{_hostingEnv.ApplicationName}.xml");

				// Sets the basePath property in the Swagger document generated
				//TODO: Check whether this is needed.
				// c.DocumentFilter<BasePathFilter>($"/CheckoutPaymentGateway/");

				// Include DataAnnotation attributes on Controller Action parameters as Swagger validation rules (e.g required, pattern, ..)
				// Use [ValidateModelState] on Actions to actually validate it in C# as well!
				c.OperationFilter<GeneratePathParamsValidationFilter>();
			});
		}

		/// <summary>
		///  ConfigureContainer is where you can register things directly
		///	 with Autofac. This runs after ConfigureServices so the things
		///	 here will override registrations made in ConfigureServices.
		///	 Don't build the container; that gets done for you by the factory.
		/// </summary>
		/// <param name="builder"></param>
		public void ConfigureContainer(ContainerBuilder builder)
		{
			// Register your own things directly with Autofac here. Don't
			// call builder.Populate(), that happens in AutofacServiceProviderFactory
			// for you.

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
				cfg.CreateMap<Payment, Repositories.PaymentsDb.Models.Payment>()
					.ForMember(dest => dest.PaymentStatusId, opt => opt.MapFrom(src => src.Status))
					.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PaymentId))
					.ReverseMap();
				cfg.CreateMap<User, Repositories.PaymentsDb.Models.User>().ReverseMap();
				cfg.CreateMap<Card, Repositories.PaymentsDb.Models.Card>().ReverseMap();
			})).AsSelf().SingleInstance();

			builder.Register(c =>
			{
				var context = c.Resolve<IComponentContext>();
				var config = context.Resolve<MapperConfiguration>();
				return config.CreateMapper(context.Resolve);
			}).As<IMapper>().InstancePerLifetimeScope();

			builder.RegisterType<PaymentService>().AsImplementedInterfaces();
			builder.RegisterType<PaymentRepo>().AsImplementedInterfaces();


			builder.RegisterType<TestBankEndpoint>().AsImplementedInterfaces();
			//builder.RegisterType<AlternateTestBankEndpoint>().AsImplementedInterfaces();

			//builder.RegisterType<TestBankEndpoint>()
			// .WithParameter(
			//	 new ResolvedParameter(
			//		 (pi, ctx) => pi.ParameterType == typeof(IBankEndpoint),
			//		 (pi, ctx) => ctx.ResolveKeyed<IBankEndpoint>()));
			//builder.RegisterType<TestBankEndpoint>()
			//		 .As<IBankEndpoint>()
			//		 .Keyed<ISender>("order");
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		/// <param name="loggerFactory"></param>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
		{
			app.UseRouting();

			app.UseStaticFiles();

			app.UseAuthorization();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment Gateway API");
			});

			//TODO: Use Https Redirection
			// app.UseHttpsRedirection();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				//TODO: Enable production exception handling (https://docs.microsoft.com/en-us/aspnet/core/fundamentals/error-handling)
				app.UseExceptionHandler("/Error");

				app.UseHsts();
			}
		}
	}
}

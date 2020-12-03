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
using Prometheus;
using System.Collections.Generic;

namespace CheckoutPaymentGateway
{
	/// <summary>
	/// Startup
	/// </summary>
	public class Startup
	{
		// Left as a const string to avoid having this in config files. 
		private const string IssuerSigningKeyString = "WeAllLoveMrCrumbsHardWork";
		private const string ConfigurationFilename = "appsettings";
		private const string ConfigurationFileType = "json";
		private const string PaymentDatabaseName = "PaymentsDbEntities";
		private readonly IWebHostEnvironment _hostingEnv;

		private IConfiguration Configuration { get; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="env"></param>
		/// <param name="configuration"></param>
		public Startup(IWebHostEnvironment env, IConfiguration configuration)
		{
			_hostingEnv = env;
			Configuration = configuration;

			// In ASP.NET 5 `env` will be an IWebHostEnvironment, not IHostingEnvironment.
			var builder = new ConfigurationBuilder()
					.SetBasePath(env.ContentRootPath)
					.AddJsonFile((ConfigurationFilename + "." + ConfigurationFilename), optional: true, reloadOnChange: true)
					.AddJsonFile($"{ConfigurationFilename}.{env.EnvironmentName}.{ConfigurationFileType}", optional: true)
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


			var jwtOptions = new JwtTokenOptions();
			var jwtOptionsRaw = Configuration.GetSection(JwtTokenOptions.JwtToken);
			jwtOptionsRaw.Bind(jwtOptions);


			var swaggerOptions = new MySwaggerOptions();
			var swaggerOptionsRaw = Configuration.GetSection(MySwaggerOptions.SwaggerGen);
			swaggerOptionsRaw.Bind(swaggerOptions);

			services.AddDbContext<PaymentsDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString(PaymentDatabaseName)));

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
					ValidAudience = jwtOptions.Issuer,
					ValidIssuer = jwtOptions.Issuer,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IssuerSigningKeyString))
				};
			});

			services.AddAuthorization(options =>
			{
				options.AddPolicy(jwtOptions.Policy,
				policy =>
				{
					policy.RequireClaim(jwtOptions.Claim.Type);
				});
			});

			#region BootstrapperForTokeIssuerToken

			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IssuerSigningKeyString));



			var authClaims = new[]
			{
				new Claim(jwtOptions.Claim.Type,jwtOptions.Claim.Value)
			};
			foreach (var audience in new[] { jwtOptions.Issuer })
			{
				var token = new JwtSecurityToken(
							 audience: audience,
							 issuer: jwtOptions.Issuer,
							 //expires: DateTime.Now.AddYears(3),
							 claims: authClaims,
							 signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
				);
				var tokenIssuer = new JwtSecurityTokenHandler().WriteToken(token);
			}

			#endregion

			services.AddSwaggerGen()
			.AddSwaggerGen(c =>
			{
				c.SwaggerDoc(swaggerOptions.Version.ToLower(), new OpenApiInfo
				{
					Version = swaggerOptions.Version.ToUpper(),
					Title = swaggerOptions.Title,
					Description = swaggerOptions.Description,
					//Contact = new OpenApiContact()
					//{
					//	Name = "Swagger Codegen Contributors",
					//	Url = new Uri("https://github.com/swagger-api/swagger-codegen"),
					//	Email = string.Empty
					//}
				});
				c.CustomSchemaIds(type => type.FullName);
				c.IncludeXmlComments($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{_hostingEnv.ApplicationName}.xml");

				// Include DataAnnotation attributes on Controller Action parameters as Swagger validation rules (e.g required, pattern, ..)
				// Use [ValidateModelState] on Actions to actually validate it in C# as well!
				c.OperationFilter<GeneratePathParamsValidationFilter>();

				c.AddSecurityDefinition(jwtOptions.Scheme, new OpenApiSecurityScheme
				{
					Description = jwtOptions.Description,
					In = ParameterLocation.Header,
					Name = jwtOptions.Name,
					Type = SecuritySchemeType.Http,
					Scheme = jwtOptions.Scheme
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement()
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = jwtOptions.Scheme
							},
							Scheme = jwtOptions.SecurityType,
							Name = jwtOptions.Scheme,
							In = ParameterLocation.Header,
						},
						new List<string>()
					}
				});
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
			})).AsSelf().SingleInstance();

			builder.Register(c =>
			{
				var context = c.Resolve<IComponentContext>();
				var config = context.Resolve<MapperConfiguration>();
				return config.CreateMapper(context.Resolve);
			}).As<IMapper>().InstancePerLifetimeScope();

			builder.RegisterType<PaymentService>().AsImplementedInterfaces();
			builder.RegisterType<PaymentRepo>().AsImplementedInterfaces();


			builder.RegisterType<TestBankEndpoint>().AsImplementedInterfaces().Keyed<IBankEndpoint>(TestBankEndpoint.TestBankName);
			builder.RegisterType<AlternateTestBankEndpoint>().AsImplementedInterfaces().Keyed<IBankEndpoint>(AlternateTestBankEndpoint.TestAlternateBankName);
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

			// Custom Metrics to count requests for each endpoint and the method
			var counter = Metrics.CreateCounter("checkoutpaymentgateway_counter", "Counts requests to the Payment Gateway API endpoints", new CounterConfiguration
			{
				LabelNames = new[] { "method", "endpoint" }
			});
			app.Use((context, next) =>
			{
				counter.WithLabels(context.Request.Method, context.Request.Path).Inc();
				return next();
			});
			//Use the Prometheus middleware
			app.UseMetricServer();
			app.UseHttpMetrics();

			app.UseHttpsRedirection();

			PrepDb.PrepPopulation(app);

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
				app.UseExceptionHandler("/Error");

				app.UseHsts();
			}
		}
	}
}

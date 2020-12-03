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
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using CheckoutPaymentGateway.Attributes;

using CheckoutPaymentGateway.Models;
using CheckoutPaymentGateway.Interfaces;
using Serilog;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Common.Models;
using PaymentGatewayService.Interfaces;
using Common.Enums;
using Newtonsoft.Json.Bson;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace CheckoutPaymentGateway.Controllers
{
	/// <summary>
	/// Handles creating and retrieving payments
	/// </summary>
	[ApiController]
	public class PaymentController : ControllerBase, IPaymentController
	{
		private const string JwtPolicy = "CheckoutTestPolicy";

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="log"></param>
		/// <param name="mapper"></param>
		/// <param name="paymentService"></param>
		public PaymentController(ILogger<PaymentController> log,
															IMapper mapper,
															IPaymentService paymentService)
		{
			Log = log;
			MyMapper = mapper;
			PaymentService = paymentService;
		}

		/// <summary>
		/// Used to test the authentication
		/// </summary>
		/// <param name="echo"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("/checkoutpaymentgateway/Echo")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = JwtPolicy)]
		[SwaggerOperation("Echo")]
		public ActionResult<string> Echo(string echo = "Echo")
		{
			return Ok(echo);
		}

		/// <summary>
		/// Generates a payment request with the gateway
		/// </summary>
		/// <remarks>Adds a payment</remarks>
		/// <param name="body">Payment to add</param>
		/// <response code="200">payment successfully created</response>
		/// <response code="400">invalid input, object invalid</response>
		/// <response code="409">an existing payment already exists</response>
		/// <response code="500">an error has occured</response>
		[HttpPost]
		[Route("/checkoutpaymentgateway/paymentrequest")]
		[ValidateModelState]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = JwtPolicy)]
		[SwaggerOperation("CreatePayment")]
		public virtual ActionResult<PaymentResponse> CreatePayment([FromBody] PaymentRequest body)
		{
			try
			{
				Log.LogDebug($"Recieved Payment request {body.Id}");

				// Grab raw json request to store for auditing and in case of error

				var payment = MyMapper.Map<Payment>(body);
				payment.Card = MyMapper.Map<Card>(body);
				payment.User = MyMapper.Map<User>(body);
				var result = PaymentService.ProcessPayment(payment);

				var response = MyMapper.Map<PaymentResponse>(result);

				if (result.Status == PaymentStatus.RequestSucceded)
				{
					Log.LogDebug($"Payment response 200 {body.Id}");
					return Ok(response);
				}
				else if (result.Status == PaymentStatus.DuplicateRequest)
				{
					Log.LogDebug($"Payment response 409 {body.Id}");
					return Conflict(response);
				}
				else if (result.Status == PaymentStatus.Error)
				{
					Log.LogDebug($"Payment response 500 {body.Id}");
					return StatusCode(500, response);
				}
				else
				{
					Log.LogDebug($"Payment response 400 {body.Id}");
					return BadRequest(response);
				}
			}
			catch (Exception ex)
			{
				Log.LogError(ex, ex.Message);
				return BadRequest(ex);
			}
		}

		/// <summary>
		/// Gets a payment information of a particualr request request 
		/// </summary>
		/// <remarks>Gets a payment</remarks>
		/// <param name="body">Payment to find</param>
		/// <response code="200">payment successfully found</response>
		/// <response code="400">invalid input, object invalid</response>
		/// <response code="404">Payment not found</response>
		/// <response code="500">an error has occured</response>
		[HttpGet]
		[Route("/checkoutpaymentgateway/getpayment")]
		[ValidateModelState]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = JwtPolicy)]
		[SwaggerOperation("GetPayment")]
		public virtual ActionResult<PaymentResponse> GetPayment(Guid body)
		{
			try
			{
				Log.LogDebug($"Finding payment {body}");

				var result = PaymentService.GetPayment(body);

				var response = MyMapper.Map<PaymentResponse>(result);

				if (result.Status == PaymentStatus.RequestSucceded)
				{
					Log.LogDebug($"Payment response 200 {body}");
					return Ok(response);
				}
				else if (result.Status == PaymentStatus.RequestDoesNotExist)
				{
					Log.LogDebug($"Payment response 404 {body}");
					return NotFound(response);
				}
				else if (result.Status == PaymentStatus.Error)
				{
					Log.LogDebug($"Payment response 500 {body}");
					return StatusCode(500, response);
				}
				else
				{
					Log.LogDebug($"Payment response 400 {body}");
					return BadRequest(response);
				}
			}
			catch (Exception ex)
			{
				Log.LogError(ex, ex.Message);
				return BadRequest(ex);
			}
		}

		#region Properties

		private ILogger<PaymentController> Log { get; }
		private IMapper MyMapper { get; }
		private IPaymentService PaymentService { get; }

		#endregion
	}
}

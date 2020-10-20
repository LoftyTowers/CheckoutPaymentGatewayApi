using AutoMapper;
using Common.Enums;
using Common.Models;
using PaymentGatewayService.Interfaces;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGatewayService.Services
{
	/// <summary>
	/// Handles payment requests and data retrieval 
	/// </summary>
	public class PaymentService : IPaymentService
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="log"></param>
		/// <param name="mapper"></param>
		/// <param name="bankApi"></param>
		public PaymentService(ILogger log, IMapper mapper, IBankEndpoint bankApi)
		{
			Log = log;
			MyMapper = mapper;
			BankApi = bankApi;
		}

		/// <summary>
		/// This method validates and stores requests and payment information
		/// </summary>
		/// <param name="paymentRequest"></param>
		/// <returns></returns>
		public Payment ProcessPayment(Payment paymentRequest)
		{
			try
			{
				//TODO: send to repo

				Log.Debug("We have arrived safely");
				return BankApi.SendPayment(paymentRequest);
			}
			catch (Exception ex)
			{
				Log.Error(ex, $"Big Bang: { paymentRequest }");
				paymentRequest.Status = PaymentStatus.RequestFailed;
				paymentRequest.IsSuccessful = false;
				paymentRequest.Message = ex.Message;
				return paymentRequest;
			}
		}


		#region Properties

		private ILogger Log { get; }
		private IMapper MyMapper { get; }
		private IBankEndpoint BankApi { get; }

		#endregion
	}
}

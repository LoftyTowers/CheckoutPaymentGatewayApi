using AcquiringBankMock.Interfaces;
using AutoMapper;
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
		public PaymentService(ILogger log, IMapper mapper, IPaymentController bankApi)
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
		public bool ProcessPayment(Payment paymentRequest)
		{
			try
			{
				Log.Debug("We have arrived safely");
				return false;
			}
			catch (Exception ex)
			{
				Log.Error(ex, $"Big Bang: { paymentRequest }");
				throw;
			}
		}


		#region Properties

		private ILogger Log { get; }
		private IMapper MyMapper { get; }
		private IPaymentController BankApi { get; }

		#endregion
	}
}

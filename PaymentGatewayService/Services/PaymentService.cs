using AutoMapper;
using Common.Enums;
using Common.Models;
using Microsoft.Extensions.Logging;
using PaymentGatewayService.Interfaces;
using Repositories.PaymentsDb.Interfaces;
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
		public PaymentService(ILogger<PaymentService> log, IPaymentRepo paymentRepo, IBankEndpoint bankApi)
		{
			Log = log;
			BankApi = bankApi;
			PaymentRepo = paymentRepo;
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
				Log.LogInformation($"Creating Payment: {paymentRequest.PaymentId}");
				PaymentRepo.StorePayment(paymentRequest);

				var bankResult = BankApi.SendPayment(paymentRequest);


				Log.LogInformation($"Updating Payment: {paymentRequest.PaymentId} with banks response");
				PaymentRepo.UpdatePayment(bankResult);

				return bankResult;
			}
			catch (Exception ex)
			{
				Log.LogError(ex, $"Big Bang: { paymentRequest }");
				paymentRequest.Status = PaymentStatus.RequestFailed;
				paymentRequest.IsSuccessful = false;
				paymentRequest.Message = ex.Message;
				return paymentRequest;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="body"></param>
		/// <returns></returns>
		public Payment GetPayment(Guid paymentId)
		{
			var paymentRequest = new Payment { PaymentId = paymentId };
			try
			{
				throw new NotImplementedException();
			}
			catch (Exception ex)
			{
				Log.LogError(ex, $"Big Bang: { paymentRequest }");
				paymentRequest.Message = ex.Message;
				return paymentRequest;
			}
		}


		#region Properties

		private ILogger Log { get; }
		private IBankEndpoint BankApi { get; }
		private IPaymentRepo PaymentRepo { get; }

		#endregion
	}
}

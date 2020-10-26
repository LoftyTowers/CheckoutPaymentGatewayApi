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
		/// <param name="paymentRepo"></param>
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
				Log.LogInformation($"Getting User Id for payment {paymentRequest.PaymentId}");
				paymentRequest.User = PaymentRepo.AddUser(paymentRequest.User);

				Log.LogInformation($"Getting Card Id for payment {paymentRequest.PaymentId}");
				paymentRequest.Card = PaymentRepo.AddCard(paymentRequest.Card, paymentRequest.User.Id);

				Log.LogInformation($"Creating Payment: {paymentRequest.PaymentId}");
				paymentRequest = PaymentRepo.StorePayment(paymentRequest);

				//If the request already exists return duplicate
				if (paymentRequest.Status == PaymentStatus.DuplicateRequest)
					return paymentRequest;

				Log.LogInformation($"Sending payment: {paymentRequest.PaymentId} to bank : {paymentRequest.RecievingBankName}");
				paymentRequest = BankApi.SendPayment(paymentRequest);


				Log.LogInformation($"Updating Payment: {paymentRequest.PaymentId} with banks response");
				PaymentRepo.UpdatePayment(paymentRequest);

				return paymentRequest;
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
		/// Finds a payment from the payment Id
		/// </summary>
		/// <param name="paymentId"></param>
		/// <returns></returns>
		public Payment GetPayment(Guid paymentId)
		{
			var paymentRequest = new Payment { PaymentId = paymentId };
			try
			{
				Log.LogInformation($"Looking for Payment: {paymentRequest.PaymentId}");
				paymentRequest = PaymentRepo.GetPayment(paymentId);
				Log.LogDebug($"Payment found: {paymentRequest.PaymentId}");
				return paymentRequest;
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

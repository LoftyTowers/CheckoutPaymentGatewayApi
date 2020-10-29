using Common.Enums;
using Common.Models;
using Microsoft.Extensions.Logging;
using PaymentGatewayService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGatewayService.BankEndpoints
{
	public class AlternateTestBankEndpoint : IBankEndpoint
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public AlternateTestBankEndpoint(ILogger<AlternateTestBankEndpoint> log)
		{
			Log = log;
		}

		/// <summary>
		/// Retireves Money and pays merchant
		/// </summary>
		/// <param name="paymentrequest"></param>
		/// <returns>PaymentResponse</returns>
		public Payment SendPayment(Payment payment)
		{
			try
			{
				payment.Status = PaymentStatus.RequestSucceded;
				payment.IsSuccessful = true;
				payment.BankPaymentId = Guid.NewGuid();
				return payment;
			}
			catch (Exception ex)
			{
				Log.LogError(ex, "Failed to process payment");
				payment.Status = PaymentStatus.Error;
				payment.IsSuccessful = false;
				payment.Message = ex.Message;
				return payment;
			}
		}

		#region Properties

		public ILogger Log { get; }

		#endregion
	}
}

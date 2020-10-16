using Common.Models;
using Microsoft.Extensions.Logging;
using PaymentGatewayService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGatewayService.BankEndpoints
{
	public class TestBankEndpoint : IBankEndpoint
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public TestBankEndpoint(ILogger log)
		{
			Log = log;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="payment"></param>
		/// <returns></returns>
		public Payment SendPayment(Payment payment)
		{
			try
			{
				switch (payment.CardNumber)
				{
					case 374245455400126:
						{
							payment.Message = "Card Declined: Insuffucent Funds";
							break;
						}
					case 378282246310005:
						{
							payment.Message = "Card Declined: Card Not Activated";
							break;
						}
					case 6250941006528599:
						{
							payment.Message = "Card Declined: Stolen/Cancelled";
							break;
						}
					case 60115564485789458:
						{
							payment.Message = "Card Declined: Invalid Card Credentials";
							break;
						}
					case 6011000991300009:
						{
							payment.Message = "Card Declined: Card Expired";
							break;
						}
					case 3566000020000410:
						{
							throw new AggregateException("Internal Error: Please try again later or contact support.");
						}
					default:
						{
							payment.IsSuccessful = true;
							break;
						}
				}
				return payment;
			}
			catch (Exception ex)
			{
				Log.LogError(ex,"Failed to process payment");
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

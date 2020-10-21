using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGatewayService.Interfaces
{
	/// <summary>
	/// Handles payment requests and data retrieval 
	/// </summary>
	public interface IPaymentService
	{
		/// <summary>
		/// This method validates and stores requests and payment information
		/// </summary>
		/// <param name="paymentRequest"></param>
		/// <returns>Payment</returns>
		Payment ProcessPayment(Payment paymentRequest);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="body"></param>
		/// <returns></returns>
		Payment GetPayment(Guid paymentId);
	}
}

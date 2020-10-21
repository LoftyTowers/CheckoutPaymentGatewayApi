using Common.Models;
using Microsoft.Extensions.Logging;
using System;

namespace Repositories.PaymentsDb.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface IPaymentRepo
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="paymentId"></param>
		/// <returns></returns>
		Payment GetPayment(Guid paymentId);
	
		/// <summary>
		/// 
		/// </summary>
		void StorePayment(Payment paymentRequest);

		/// <summary>
		/// 
		/// </summary>
		void UpdatePayment(Payment paymentRequest);
		/// <summary>
		/// 
		/// </summary>
		void StoreRawPaymentRequest();
	}
}
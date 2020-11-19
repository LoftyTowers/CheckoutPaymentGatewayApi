using Common.Models;
using Microsoft.Extensions.Logging;
using System;

namespace Repositories.PaymentsDb.Interfaces
{
	/// <summary>
	/// contians all the datebase calls for Payments
	/// </summary>
	public interface IPaymentRepo
	{
		/// <summary>
		/// Gets a payment by PAymentId
		/// </summary>
		/// <param name="paymentId"></param>
		/// <returns></returns>
		Payment GetPayment(Guid paymentId);

		/// <summary>
		/// adds a user
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		Common.Models.User AddUser(Common.Models.User user);

		/// <summary>
		/// Adds a new payment
		/// </summary>
		Payment StorePayment(Payment paymentRequest);

		/// <summary>
		/// updates a paymentr with BankPaymentId and success/failure information
		/// </summary>
		void UpdatePayment(Payment paymentRequest);
		/// <summary>
		/// stores the raw json request from the api
		/// </summary>
		void StoreRawPaymentRequest();
	}
}
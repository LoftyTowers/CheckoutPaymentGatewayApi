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
		/// adds a card
		/// </summary>
		/// <param name="card"></param>
		/// <returns></returns>
		Common.Models.Card AddCard(Common.Models.Card card, Guid userId);

		/// <summary>
		/// Adds a new payment
		/// </summary>
		Payment StorePayment(Payment paymentRequest);

		/// <summary>
		/// updates a paymentr with BankPaymentId and success/failure information
		/// </summary>
		Payment UpdatePayment(Payment paymentRequest);
	}
}
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
		/// <param name="user"></param>
		/// <returns></returns>
		Common.Models.User AddUser(Common.Models.User user);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="card"></param>
		/// <returns></returns>
		Common.Models.Card AddCard(Common.Models.Card card, Guid userId);

		/// <summary>
		/// 
		/// </summary>
		Payment StorePayment(Payment paymentRequest);

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
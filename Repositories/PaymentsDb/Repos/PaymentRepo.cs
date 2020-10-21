using Common.Models;
using Microsoft.Extensions.Logging;
using Repositories.PaymentsDb.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.PaymentsDb.Repos
{
	/// <summary>
	/// 
	/// </summary>
	public class PaymentRepo : IPaymentRepo
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="log"></param>
		public PaymentRepo(ILogger<PaymentRepo> log)
		{
			Log = log;
		}

		/// <summary>
		/// 
		/// </summary>
		public void StoreRawPaymentRequest()
		{
			try
			{
				throw new NotImplementedException();
			}
			catch (Exception ex)
			{
				Log.LogError(ex, "");
				throw ex;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public void StorePayment(Payment paymentRequest)
		{
			try
			{
				throw new NotImplementedException();
			}
			catch (Exception ex)
			{
				Log.LogError(ex, "");
				throw ex;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="paymentId"></param>
		/// <returns></returns>
		public Payment GetPayment(Guid paymentId)
		{
			try
			{
				throw new NotImplementedException();
			}
			catch (Exception ex)
			{
				Log.LogError(ex, "");
				throw;
			}
		}

		public void UpdatePayment(Payment paymentRequest)
		{
			try
			{
				throw new NotImplementedException();
			}
			catch (Exception ex)
			{
				Log.LogError(ex, "");
				throw;
			}
		}

		#region Properties

		public ILogger Log { get; }

		#endregion
	}
}

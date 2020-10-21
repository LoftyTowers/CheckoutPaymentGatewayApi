using AutoMapper;
using Common.Models;
using Microsoft.Extensions.Logging;
using Repositories.PaymentsDb.DbContexts;
using Repositories.PaymentsDb.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

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
		public PaymentRepo(ILogger<PaymentRepo> log, IMapper mapper, PaymentsDbContext context)
		{
			Log = log;
			MyMapper = mapper;
			Context = context;
		}

		/// <summary>
		/// 
		/// </summary>
		public void StoreRawPaymentRequest()
		{
			using (Context)
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
		}

		/// <summary>
		/// 
		/// </summary>
		public void StorePayment(Payment paymentRequest)
		{
			using (Context)
			{
				try
				{
					Context.Payments.Add(MyMapper.Map<Models.Payment>(paymentRequest));
					Context.SaveChanges();
				}
				catch (Exception ex)
				{
					Log.LogError(ex, "");
					throw ex;
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="paymentId"></param>
		/// <returns></returns>
		public Payment GetPayment(Guid paymentId)
		{
			using (Context)
			{
				try
				{
					return MyMapper.Map<Payment>(Context.Payments.Find(paymentId));
				}
				catch (Exception ex)
				{
					Log.LogError(ex, "");
					throw;
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="paymentRequest"></param>
		public void UpdatePayment(Payment paymentRequest)
		{
			using (Context)
			{ 
				try
				{
					var payment = Context.Payments.First(p => p.Id == paymentRequest.PaymentId);
					payment.IsSuccessful = payment.IsSuccessful;
					payment.Message = paymentRequest.Message;
					payment.StatusId = paymentRequest.Status;
					payment.Updated = DateTime.UtcNow;

					Context.SaveChanges();
				}
				catch (Exception ex)
				{
					Log.LogError(ex, "");
					throw;
				}
		}
		}

		#region Properties

		private ILogger Log { get; }
		private IMapper MyMapper { get; }
		private PaymentsDbContext Context { get; }

		#endregion
	}
}

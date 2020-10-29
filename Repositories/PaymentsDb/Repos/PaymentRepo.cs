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
using Microsoft.EntityFrameworkCore;
using Common.Enums;
using Microsoft.Data.SqlClient;

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
		public PaymentRepo(ILogger<PaymentRepo> log, IMapper mapper, DbContextOptions<PaymentsDbContext> contextOptions)
		{
			Log = log;
			MyMapper = mapper;
			ContextOptions = contextOptions;
		}

		/// <summary>
		/// stores the raw json request from the api
		/// </summary>
		public void StoreRawPaymentRequest()
		{
			using (var context = new PaymentsDbContext(ContextOptions))
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
		/// adds a user
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public Common.Models.User AddUser(Common.Models.User user)
		{
			using (var context = new PaymentsDbContext(ContextOptions))
			{
				try
				{
					var dbUser = context.Users.FirstOrDefault(u => u.Fullname == user.Fullname && u.DateOfBirth == user.DateOfBirth);
					if (dbUser == null || string.IsNullOrWhiteSpace(dbUser.Id.ToString()))
					{
						user.Id = Guid.NewGuid();
						context.Users.Add(MyMapper.Map<Models.User>(user));
						context.SaveChanges();
					}
					else
					{
						user.Id = dbUser.Id;
					}
					return user;
				}
				catch (Exception ex)
				{
					Log.LogError(ex, "");
					throw ex;
				}
			}
		}

		/// <summary>
		/// adds a card
		/// </summary>
		/// <param name="card"></param>
		/// <returns></returns>
		public Common.Models.Card AddCard(Common.Models.Card card, Guid userId)
		{
			using (var context = new PaymentsDbContext(ContextOptions))
			{
				try
				{
					var result = context.Database.ExecuteSqlInterpolated($"AddCard {card.CardNumber},{card.CVC},{card.ExpiryDate},{card.BankName},{userId}");

					var toReturn = MyMapper.Map<Common.Models.Card>(result);

					return toReturn;
				}
				catch (Exception ex)
				{
					Log.LogError(ex, "");
					throw ex;
				}
			}
		}

		/// <summary>
		/// Adds a new payment
		/// </summary>
		public Common.Models.Payment StorePayment(Common.Models.Payment paymentRequest)
		{
			using (var context = new PaymentsDbContext(ContextOptions))
			{
				try
				{
					var dbPayment = context.Payments.FirstOrDefault(c => c.Id == paymentRequest.PaymentId);
					if (dbPayment == null || string.IsNullOrWhiteSpace(dbPayment.Id.ToString()))
					{

						Log.LogInformation($"Adding the payment to the database: {paymentRequest.PaymentId}");
						var newPayment = MyMapper.Map<Models.Payment>(paymentRequest);
						newPayment.CardId = paymentRequest.Card.Id;
						context.Payments.Add(newPayment);
						context.SaveChanges();
					}
					else
					{
						Log.LogWarning($"Duplicate Payment: {paymentRequest.PaymentId}");
						paymentRequest.Status = Common.Enums.PaymentStatus.DuplicateRequest;
						paymentRequest.IsSuccessful = false;
						paymentRequest.Message = $"Duplicate payment request";
					}

					return paymentRequest;

				}
				catch (Exception ex)
				{
					Log.LogError(ex, "");
					throw ex;
				}
			}
		}

		/// <summary>
		/// Gets a payment by PAymentId
		/// </summary>
		/// <param name="paymentId"></param>
		/// <returns></returns>
		public Payment GetPayment(Guid paymentId)
		{
			using (var context = new PaymentsDbContext(ContextOptions))
			{
				try
				{
					return MyMapper.Map<Common.Models.Payment>(context.Payments.Find(paymentId));
				}
				catch (Exception ex)
				{
					Log.LogError(ex, "");
					return new Payment
					{
						PaymentId = paymentId,
						IsSuccessful = false,
						Status = PaymentStatus.Error
					};
				}
			}
		}

		/// <summary>
		/// updates a paymentr with BankPaymentId and success/failure information
		/// </summary>
		public void UpdatePayment(Payment paymentRequest)
		{
			using (var context = new PaymentsDbContext(ContextOptions))
			{
				try
				{
					Log.LogInformation($"Updating Payment: {paymentRequest.PaymentId}");
					var payment = context.Payments.First(p => p.Id == paymentRequest.PaymentId);
					payment.BankPaymentId = paymentRequest.BankPaymentId;
					payment.IsSuccessful = payment.IsSuccessful;
					payment.Message = paymentRequest.Message;
					payment.PaymentStatusId = paymentRequest.Status;
					payment.Updated = DateTime.UtcNow;
					payment.RequestCompleted = paymentRequest.RequestCompleted.GetValueOrDefault();

					context.SaveChanges();
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
		private DbContextOptions<PaymentsDbContext> ContextOptions { get; }

		#endregion
	}
}

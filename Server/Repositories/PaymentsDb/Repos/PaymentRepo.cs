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
using System.Text.RegularExpressions;

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
					Log.LogError(ex, $"Failed to create user");
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
					var cardnumber = card.CardNumber;
					card.CardNumber = MaskCardNumber(cardnumber);
					var dbCard = context.Cards.FirstOrDefault(c => c.CardNumber == card.CardNumber && c.CVC == card.CVC && c.UserId == userId && c.ExpiryDate == card.ExpiryDate);
					if (dbCard == null || string.IsNullOrWhiteSpace(dbCard.Id.ToString()))
					{
						card.Id = Guid.NewGuid();
						var newCard = MyMapper.Map<Models.Card>(card);
						newCard.UserId = userId;
						context.Cards.Add(newCard);
						context.SaveChanges();
					}
					else
					{
						card.Id = dbCard.Id;
					}
					return card;
					//var toReturn = MyMapper.Map<Common.Models.Card>(result);

					//return toReturn;
				}
				catch (Exception ex)
				{
					Log.LogError(ex, "");
					Log.LogError(ex, $"Failed to create user");
					throw ex;
				}
			}
		}

		private string MaskCardNumber(string cardNumber)
		{
			string pattern = @"\d(?=\d{4})";
			string substitution = @"*";
			string input = @"374245455400126";

			Regex regex = new Regex(pattern);
			return regex.Replace(input, substitution);
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
					Log.LogError(ex, "Failed to store payment");
					paymentRequest.Status = PaymentStatus.PaymentNotStored;
					return paymentRequest;
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
					var defaultResponse = new Common.Models.Payment
					{
						PaymentId = paymentId,
						Status = PaymentStatus.RequestDoesNotExist,
						Message = $"The payment: {paymentId} does not exist"
					};
					var payment = (from p in context.Payments
												 join c in context.Cards on p.CardId equals c.Id
												 join u in context.Users on c.UserId equals u.Id
												 where p.Id == paymentId
												 select new Common.Models.Payment
												 {
													 Amount = p.Amount,
													 BankPaymentId = p.BankPaymentId,
													 CurrencyCode = p.CurrencyCode,
													 FullName = u.Fullname,
													 IsSuccessful = p.IsSuccessful,
													 Message = p.Message,
													 PaymentId = p.Id,
													 RequestCompleted = p.RequestCompleted,
													 RequestDate = p.RequestDate,
													 Status = p.PaymentStatusId,
													 CardExpiryDate = c.ExpiryDate,
													 CardNumber = c.CardNumber,
													 CVC = c.CVC
												 }).FirstOrDefault();

					if (payment == null)
						payment = defaultResponse;

					return payment;
				}
				catch (Exception ex)
				{
					Log.LogError(ex, $"Failed to get payment information {paymentId}");
					return new Payment
					{
						PaymentId = paymentId,
						IsSuccessful = false,
						Status = PaymentStatus.Error,
						Message = ex.Message
					};
				}
			}
		}

		/// <summary>
		/// updates a paymentr with BankPaymentId and success/failure information
		/// </summary>
		public Payment UpdatePayment(Payment paymentRequest)
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

					return paymentRequest;
				}
				catch (Exception ex)
				{
					Log.LogError(ex, $"Failed tgo Update the payment {paymentRequest.PaymentId}");
					paymentRequest.Message = ex.Message;
					return paymentRequest;
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

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
		/// 
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
		/// 
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
		/// 
		/// </summary>
		/// <param name="card"></param>
		/// <returns></returns>
		public Common.Models.Card AddCard(Common.Models.Card card, Guid userId)
		{
			using (var context = new PaymentsDbContext(ContextOptions))
			{
				try
				{
					var dbCard = context.Cards.FirstOrDefault(c => c.CardNumber == card.CardNumber);
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
		public Common.Models.Payment StorePayment(Common.Models.Payment paymentRequest)
		{
			using (var context = new PaymentsDbContext(ContextOptions))
			{
				try
				{
					var dbPayment = context.Payments.FirstOrDefault(c => c.Id == paymentRequest.PaymentId);
					if (dbPayment == null || string.IsNullOrWhiteSpace(dbPayment.Id.ToString()))
					{
						var newPayment = MyMapper.Map<Models.Payment>(paymentRequest);
						newPayment.CardId = paymentRequest.Card.Id;
						context.Payments.Add(newPayment);
						context.SaveChanges();
					}
					else
					{
						//paymentRequest.Status = Common.Enums.PaymentStatus.DuplicateRequest;
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
		/// 
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
			using (var context = new PaymentsDbContext(ContextOptions))
			{
				try
				{
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

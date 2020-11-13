using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
	public class Payment
	{
		/// <summary>
		/// Unique Identifier of the payment from the merchant
		/// </summary>
		public Guid PaymentId { get; set; }

		/// <summary>
		/// Unique Identifier of the payment from the bank
		/// </summary>
		public Guid? BankPaymentId { get; set; }

		/// <summary>
		/// The currency code the transaction is made in
		/// </summary>
		public string CurrencyCode { get; set; }

		/// <summary>
		/// The amount of the transaction
		/// </summary>
		public decimal? Amount { get; set; }

		/// <summary>
		/// The cvc customers card
		/// </summary>
		public int? CVC { get; set; }

		/// <summary>
		/// The CardNumber for the transaction
		/// </summary>
		public long? CardNumber { get; set; }

		/// <summary>
		/// The FullName of the customer as shown on the card
		/// </summary>
		public string FullName { get; set; }

		/// <summary>
		/// The expiry date of the customers card
		/// </summary>
		public DateTime? CardExpiryDate { get; set; }

		/// <summary>
		/// The date the transaction was initilised
		/// </summary>
		public DateTime? RequestDate { get; set; }

		/// <summary>
		/// Gets or Sets RequestCompleted
		/// </summary>
		public DateTime? RequestCompleted { get; set; }

		/// <summary>
		/// gets or Sets Status
		/// </summary>
		public PaymentStatus Status { get; set; }

		/// <summary>
		/// Gets or Sets IsSuccessful
		/// </summary>
		public bool IsSuccessful { get; set; }

		/// <summary>
		/// Gets or Sets Message
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Gets or Sets the User
		/// </summary>
		public User User { get; set; }

		/// <summary>
		/// Gets or Sets Card
		/// </summary>
		public Card Card { get; set; }

		/// <summary>
		/// NAme of the bank the payment is made from
		/// </summary>
		public string SendingBankName { get; set; }

		/// <summary>
		/// Name of the bank the payment is sent to
		/// </summary>
		public string RecievingBankName { get; set; }
	}
}

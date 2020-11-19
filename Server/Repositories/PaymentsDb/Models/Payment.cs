using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repositories.PaymentsDb.Models
{
	public class Payment
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		public Guid Id { get; set; }
		public Guid? BankPaymentId { get; set; }
		public string CurrencyCode { get; set; }
		public decimal Amount { get; set; }
		public DateTime RequestDate { get; set; }
		public DateTime Updated { get; set; }
		public DateTime? RequestCompleted { get; set; }
		public bool IsSuccessful { get; set; }
		public string Message { get; set; }

		public Guid UserId { get; set; }
		public Common.Enums.PaymentStatus PaymentStatusId { get; set; }
	}
}

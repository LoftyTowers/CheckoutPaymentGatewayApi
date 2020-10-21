using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.PaymentsDb.Models
{
	public class Payment
	{
		public Guid Id { get; set; }
		public Guid CardId { get; set; }
		public string CurrencyCode { get; set; }
		public decimal Amount { get; set; }
		public DateTime RequestDate { get; set; }
		public DateTime Updated { get; set; }
		public DateTime RequestCompleted { get; set; }
		public Common.Enums.PaymentStatus StatusId { get; set; }
		public bool IsSuccessful { get; set; }
		public string Message { get; set; }


		public ICollection<Card> Cards { get; set; }

		public ICollection<PaymentStatus> PaymentStatus { get; set; }
	}
}

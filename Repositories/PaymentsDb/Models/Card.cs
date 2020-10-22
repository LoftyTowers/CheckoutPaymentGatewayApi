using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.PaymentsDb.Models
{
	public class Card
	{
		public Guid Id { get; set; }
		public long CardNumber { get; set; }
		public int CVC { get; set; }
		public DateTime ExpiryDate { get; set; }

		public Guid UserId { get; set; }

		public ICollection<Payment> Payments { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.PaymentsDb.Models
{
	public class PaymentStatus
	{
		public int Id { get; set; }
		/// <summary>
		/// Status Description
		/// </summary>
		public string StatusDesc { get; set; }
	}
}

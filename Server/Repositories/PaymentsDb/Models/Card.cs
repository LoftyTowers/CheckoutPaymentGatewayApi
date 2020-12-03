using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Repositories.PaymentsDb.Models
{
	public class Card
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		public Guid Id { get; set; }
		public string CardNumber { get; set; }
		public int CVC { get; set; }
		public DateTime ExpiryDate { get; set; }
		public string BankName { get; set; }


		public Guid UserId { get; set; }
		public ICollection<Payment> Payments { get; set; }
	}
}

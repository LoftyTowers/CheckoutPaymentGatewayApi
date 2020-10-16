using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
	public class Payment
	{/// <summary>
   /// Gets or Sets Id
   /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or Sets MerchantId
    /// </summary>
    public string MerchantId { get; set; }

    /// <summary>
    /// Gets or Sets Amount
    /// </summary>
    public decimal? Amount { get; set; }

    /// <summary>
    /// Gets or Sets RequestDate
    /// </summary>
    public DateTime? RequestDate { get; set; }

    /// <summary>
    /// Gets or Sets RequestCompleted
    /// </summary>
    public DateTime? RequestCompleted { get; set; }

    /// <summary>
    /// 
    /// </summary>
		public PaymentStatus Status { get; set; }

		/// <summary>
		/// Gets or Sets Complete
		/// </summary>
		public bool Complete { get; set; }
  }
}

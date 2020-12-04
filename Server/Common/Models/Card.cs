using System;

namespace Common.Models
{
	/// <summary>
	/// Contains all card relevent information
	/// </summary>
	public class Card
	{
		/// <summary>
		/// Database Generated unique Id for this card
		/// </summary>
		public Guid Id { get; set; }
		/// <summary>
		/// The 16 digit unmber on the front of the card
		/// </summary>
		public string CardNumber { get; set; }
		/// <summary>
		/// CVC on the back of the card
		/// </summary>
		public int CVC { get; set; }
		/// <summary>
		/// The expirary date on the card
		/// </summary>
		public DateTime ExpiryDate { get; set; }
		/// <summary>
		/// Name of the bank on the card
		/// </summary>
		public string BankName { get; set; }
	}
}
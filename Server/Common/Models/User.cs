using System;

namespace Common.Models
{
	/// <summary>
	/// Contains all user relevent information
	/// </summary>
	public class User
	{
		/// <summary>
		/// Database Generated unique Id for this user
		/// </summary>
		public Guid Id { get; set; }
		/// <summary>
		/// NAme of the user
		/// </summary>
		public string Fullname { get; set; }
		/// <summary>
		/// date of the birth of the user
		/// </summary>
		public DateTime DateOfBirth { get; set; }
	}
}
using System;

namespace Common.Models
{
	public class User
	{
		public Guid Id { get; set; }
		public string Fullname { get; set; }
		public DateTime DateOfBirth { get; set; }
	}
}
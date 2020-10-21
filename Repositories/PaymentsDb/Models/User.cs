using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.PaymentsDb.Models
{
	public class User
	{
		public Guid Id { get; set; }
		public string Fullname { get; set; }
	}
}

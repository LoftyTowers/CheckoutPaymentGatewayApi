using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
	public class JwtTokenOptions
	{
		public const string JwtToken = "JwtToken";

		public string Issuer { get; set; }
		public string Policy { get; set; }
		public JwtClaim Claim { get; set; }
		public string Scheme { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string SecurityType { get; set; }
	}
	public class JwtClaim
	{
		public string Type { get; set; }
		public string Value { get; set; }
	}
}

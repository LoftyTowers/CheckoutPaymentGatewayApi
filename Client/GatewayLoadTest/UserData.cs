using System;
using System.Collections.Generic;
using System.Text;

namespace GatewayLoadTest
{
	public class UserData
	{
		public UserData(int userId)
		{
			switch (userId)
			{
				case 1:
					Cvc = 111;
					CardNumber = 374245455400126;
					FullName = "Jim Frost";
					DateOfBirth = DateTime.Parse("1977-08-22");
					CardExpiryDate = DateTime.Parse("2022-10-22");
					SendingBankName = "AlternateBank";
					RecievingBankName = "AlternateBank";
					break;
				case 2:
					Cvc = 222;
					CardNumber = 5425233430109903;
					FullName = "John Doe";
					DateOfBirth = DateTime.Parse("1980-04-02");
					CardExpiryDate = DateTime.Parse("2024-03-11");
					SendingBankName = "TestBank";
					RecievingBankName = "TestBank";
					break;
				case 3:
					Cvc = 333;
					CardNumber = 374245455400126;
					FullName = "Cindy Vapid";
					DateOfBirth = DateTime.Parse("1990-01-30");
					CardExpiryDate = DateTime.Parse("2021-06-18");
					SendingBankName = "TestBank";
					RecievingBankName = "TestBank";
					break;
				case 4:
					Cvc = 444;
					CardNumber = 378282246310005;
					FullName = "Coconut Styles";
					DateOfBirth = DateTime.Parse("1952-03-09");
					CardExpiryDate = DateTime.Parse("2020-12-15");
					SendingBankName = "TestBank";
					RecievingBankName = "TestBank";
					break;
				case 5:
					Cvc = 555;
					CardNumber = 6250941006528599;
					FullName = "Bernie Crampons";
					DateOfBirth = DateTime.Parse("2000-01-01");
					CardExpiryDate = DateTime.Parse("2022-06-22");
					SendingBankName = "TestBank";
					RecievingBankName = "TestBank";
					break;
				case 6:
					Cvc = 666;
					CardNumber = 60115564485789458;
					FullName = "Zoltan Zoltan";
					DateOfBirth = DateTime.Parse("1996-09-29");
					CardExpiryDate = DateTime.Parse("2022-01-12");
					SendingBankName = "TestBank";
					RecievingBankName = "TestBank";
					break;
				case 7:
					Cvc = 777;
					CardNumber = 6011000991300009;
					FullName = "Veronica Hammock";
					DateOfBirth = DateTime.Parse("1993-04-20");
					CardExpiryDate = DateTime.Parse("2025-11-14");
					SendingBankName = "TestBank";
					RecievingBankName = "TestBank";
					break;
				case 8:
					Cvc = 888;
					CardNumber = 3566000020000410;
					FullName = "Chet Vacant";
					DateOfBirth = DateTime.Parse("1999-06-08");
					CardExpiryDate = DateTime.Parse("2023-05-05");
					SendingBankName = "TestBank";
					RecievingBankName = "TestBank";
					break;
				default:
					Cvc = 999;
					CardNumber = 5425233430109903;
					FullName = "Simon Doe";
					DateOfBirth = DateTime.Parse("1970-05-16");
					CardExpiryDate = DateTime.Parse("2024-09-11");
					SendingBankName = "TestBank";
					RecievingBankName = "TestBank";
					break;
			}
		}
		public int Cvc { get; set; }
		public long CardNumber { get; set; }
		public string FullName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public DateTime CardExpiryDate { get; set; }
		public string SendingBankName { get; set; }
		public string RecievingBankName { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GatewayLoadTest
{
	public class UserData
	{
		public UserData(int userId, int failureChance)
		{
			//if the failure chance is greater than 1 then set case to give a positive outcome 
			//in order to replicate 1% chance of a failed payment
			if (failureChance > 1)
				userId = 1;
			else if (failureChance > 50)
				userId = 2;

			switch (userId)
			{
				case 1:
					//Success
					Cvc = 111;
					CardNumber = 927245455675248;
					FullName = "Jim Frost";
					DateOfBirth = DateTime.Parse("1977-08-22");
					CardExpiryDate = DateTime.Parse("2022-10-22");
					SendingBankName = "TestBank";
					RecievingBankName = "TestBank";
					break;
				case 2:
					//Success
					Cvc = 222;
					CardNumber = 5425233430109903;
					FullName = "John Doe";
					DateOfBirth = DateTime.Parse("1980-04-02");
					CardExpiryDate = DateTime.Parse("2024-03-11");
					SendingBankName = "AlternateBank";
					RecievingBankName = "AlternateBank";
					break;
				case 3:
					// InsuffucentFunds
					Cvc = 333;
					CardNumber = 374245455400126;
					FullName = "Cindy Vapid";
					DateOfBirth = DateTime.Parse("1990-01-30");
					CardExpiryDate = DateTime.Parse("2021-06-18");
					SendingBankName = "TestBank";
					RecievingBankName = "TestBank";
					break;
				case 4:
					//CardNotActivated
					Cvc = 444;
					CardNumber = 378282246310005;
					FullName = "Coconut Styles";
					DateOfBirth = DateTime.Parse("1952-03-09");
					CardExpiryDate = DateTime.Parse("2020-12-15");
					SendingBankName = "TestBank";
					RecievingBankName = "TestBank";
					break;
				case 5:
					//StolenCancelled
					Cvc = 555;
					CardNumber = 6250941006528599;
					FullName = "Bernie Crampons";
					DateOfBirth = DateTime.Parse("2000-01-01");
					CardExpiryDate = DateTime.Parse("2022-06-22");
					SendingBankName = "TestBank";
					RecievingBankName = "TestBank";
					break;
				case 6:
					//InvalidCardCredentials
					Cvc = 666;
					CardNumber = 60115564485789458;
					FullName = "Zoltan Zoltan";
					DateOfBirth = DateTime.Parse("1996-09-29");
					CardExpiryDate = DateTime.Parse("2022-01-12");
					SendingBankName = "TestBank";
					RecievingBankName = "TestBank";
					break;
				case 7:
					//CardExpired
					Cvc = 777;
					CardNumber = 6011000991300009;
					FullName = "Veronica Hammock";
					DateOfBirth = DateTime.Parse("1993-04-20");
					CardExpiryDate = DateTime.Parse("2025-11-14");
					SendingBankName = "TestBank";
					RecievingBankName = "TestBank";
					break;
				case 8:
					//Error
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

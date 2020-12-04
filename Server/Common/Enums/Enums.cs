using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Enums
{
	/// <summary>
	/// options for the status of a payment
	/// </summary>
	public enum PaymentStatus
	{
		Unknown = 0,
		RequestRecieved = 10,
		RequestSent = 20,
		RequestSucceded = 30,
		RequestFailed = 999,
		DuplicateRequest = 1009,
		RequestDoesNotExist = 1019,
		InsuffucentFunds = 1029,
		CardNotActivated = 1039,
		StolenCancelled = 1049,
		InvalidCardCredentials = 1059,
		CardExpired = 1069,
		PaymentNotStored = 1100,
		Error = 9999
	}
}

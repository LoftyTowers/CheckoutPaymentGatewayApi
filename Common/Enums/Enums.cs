using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Enums
{
	public enum PaymentStatus
	{
		Unknown = 0,
		RequestRecieved = 10,
		RequestSent = 20,
		RequestSucceded = 30,
		RequestFailed = 99
	}
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GatewayLoadTest
{
	public static class Extension
	{
		public static double GetRandomNumber(this Random random, double minimum, double maximum)
		{
			return Math.Round((random.NextDouble() * (maximum - minimum) + minimum), 2);
		}
	}
}

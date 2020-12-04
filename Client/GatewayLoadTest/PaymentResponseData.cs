using PaymentGatewayAPIClient.Model;
using System.Collections.Generic;

namespace GatewayLoadTest
{
	public class PaymentResponseData
	{
		public int TotalTime { get; set; }
		public int SuccessCount { get; set; }
		public int FailedCount { get; set; }
		public List<CheckoutPaymentGatewayModelsPaymentResponse> Errors { get; set; }
	}
}
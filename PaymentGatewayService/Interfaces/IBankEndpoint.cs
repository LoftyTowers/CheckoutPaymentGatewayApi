using Common.Models;

namespace PaymentGatewayService.Interfaces
{
	/// <summary>
	/// Banking 
	/// </summary>
	public interface IBankEndpoint
	{
		/// <summary>
		/// Retireves Money and pays merchant
		/// </summary>
		/// <param name="paymentrequest"></param>
		/// <returns>PaymentResponse</returns>
		Payment SendPayment(Payment paymentrequest);
	}
}
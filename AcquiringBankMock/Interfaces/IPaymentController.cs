using AcquiringBankMock.Models;

namespace AcquiringBankMock.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface IPaymentController
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="paymentrequest"></param>
		/// <returns>PaymentResponse</returns>
		PaymentResponse SendPayment(PaymentRequest paymentrequest);
	}
}
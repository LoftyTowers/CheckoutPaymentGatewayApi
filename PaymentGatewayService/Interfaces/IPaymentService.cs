using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGatewayService.Interfaces
{
	public interface IPaymentService
	{
		bool ProcessPayment(Payment paymentRequest);
	}
}

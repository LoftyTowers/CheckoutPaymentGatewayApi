using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcquiringBankMock.Interfaces;
using AcquiringBankMock.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcquiringBankMock.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	[ApiController]
	public class PaymentController : ControllerBase, IPaymentController
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="paymentrequest"></param>
		/// <returns></returns>
		public PaymentResponse SendPayment(PaymentRequest paymentrequest)
		{
			try
			{
				return new PaymentResponse();
			}
			catch (Exception ex)
			{
				return new PaymentResponse();
			}
		}
	}
}

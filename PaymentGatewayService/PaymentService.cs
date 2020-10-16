using AutoMapper;
using Common.Models;
using PaymentGatewayService.Interfaces;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGatewayService
{
	public class PaymentService : IPaymentService
	{
		public PaymentService(ILogger log, IMapper mapper)
		{
			Log = log;
			MyMapper = mapper;
		}

		public bool ProcessPayment(Payment paymentRequest)
		{
			try
			{
				Log.Debug("We have arrived safely");
				return false;
			}
			catch (Exception ex)
			{
				Log.Error(ex, $"Big Bang: { paymentRequest }");
				throw;
			}
		}


		#region Properties

		private ILogger Log { get; }
		private IMapper MyMapper { get; }

		#endregion
	}
}

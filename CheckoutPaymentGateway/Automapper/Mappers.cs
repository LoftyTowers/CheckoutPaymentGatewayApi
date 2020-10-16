using AutoMapper;
using CheckoutPaymentGateway.Models;
using Common.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Automapper
{
	public class Mappers
	{
		public Mappers(ILogger log)
		{
			Log = log;
		}

		public MapperConfiguration CreateMappers()
		{
			return new MapperConfiguration(cfg => {
				cfg.CreateMap<PaymentRequest, Payment>().ReverseMap();
			});
		}

		#region Properties

		private ILogger Log { get; }

		#endregion
	}
}

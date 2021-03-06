﻿using Microsoft.Extensions.Logging;
using PaymentGatewayAPIClient.Interfaces;
using PaymentGatewayAPIClient.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace GatewayLoadTest
{
	public class PaymentsSetup
	{
		public PaymentsSetup(ILogger<PaymentsSetup> log, IPaymentApi paymentApi)
		{
			Log = log;
			PaymentApiInstance = paymentApi;
		}

		public List<CheckoutPaymentGatewayModelsPaymentRequest> GeneratePayments(int numberOfPayments)
		{
			var payments = new List<CheckoutPaymentGatewayModelsPaymentRequest>();

			for (var payment = 0; payment < numberOfPayments; payment++)
			{
				payments.Add(GeneratePayment(string.Empty));
			}

			return payments;
		}

		public CheckoutPaymentGatewayModelsPaymentRequest GeneratePayment(string cardNumber)
		{
			Random random = new Random();
			var user = new UserData(random.Next(0, 9), random.Next(0, 100));
			return new CheckoutPaymentGatewayModelsPaymentRequest(
					 Guid.NewGuid(), // Id
					 "GDP",// CurrencyCode
					 random.GetRandomNumber(1, 999.99),// Amount
					 user.Cvc,// Cvc
					 string.IsNullOrWhiteSpace(cardNumber) ? user.CardNumber : cardNumber,// CardNumber
					 user.FullName,// FullName
					 user.DateOfBirth,// DateOfBirth
					 user.CardExpiryDate,// CardExpiryDate
					 DateTime.Now,// RequestDate
					 user.SendingBankName,// SendingBankName
					 user.RecievingBankName// RecievingBankName
				 );
		}

		public PaymentResponseData SendPayments(List<CheckoutPaymentGatewayModelsPaymentRequest> payments)
		{
			var response = new PaymentResponseData()
			{
				Errors = new List<CheckoutPaymentGatewayModelsPaymentResponse>(),
				FailedCount = 0,
				SuccessCount = 0,
				TotalTime = 0
			};
			foreach (var payment in payments)
			{
				var apiResponse = PaymentApiInstance.CheckoutpaymentgatewayPaymentrequestPost(payment);

				if (!apiResponse.IsSuccessful.GetValueOrDefault())
				{
					response.Errors.Add(apiResponse);
					response.FailedCount++;
				}
				else
					response.SuccessCount++;
			}

			return response;
		}

		public string StoreErrors(List<CheckoutPaymentGatewayModelsPaymentResponse> errors)
		{
			try
			{
				string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

				// Append text to an existing file named "WriteLines.txt".
				using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "WriteLines.txt"), true))
				{
					outputFile.WriteLine(ToJsonString(errors));
				}
				return docPath;
			}
			catch (Exception ex)
			{
				var errorMessage = "Failed to write payment errors to file";
				Log.LogError(ex, errorMessage);
				return errorMessage;
			}
		}

		public string ToJsonString(object value)
		{
			var jsonSerializer = new DataContractJsonSerializer(value.GetType());
			var jsonResponse = string.Empty;
			using (var stream = new MemoryStream())
			{
				jsonSerializer.WriteObject(stream, value);
				jsonResponse = Encoding.Default.GetString(stream.ToArray());
			}

			return jsonResponse.Replace("'", @"\'");
		}

		#region Properties

		public static ILogger<PaymentsSetup> Log { get; set; }
		public IPaymentApi PaymentApiInstance { get; }

		#endregion
	}
}

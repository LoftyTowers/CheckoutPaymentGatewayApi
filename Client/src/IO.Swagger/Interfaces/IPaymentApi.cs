using IO.Swagger.Client;
using IO.Swagger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO.Swagger.Interfaces
{
	/// <summary>
	/// Represents a collection of functions to interact with the API endpoints
	/// </summary>
	public interface IPaymentApi : IApiAccessor
	{
		#region Synchronous Operations
		/// <summary>
		/// Used to test the authentication
		/// </summary>
		/// <remarks>
		/// 
		/// </remarks>
		/// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
		/// <param name="echo"> (optional)</param>
		/// <returns>string</returns>
		string CheckoutpaymentgatewayEchoGet(string echo = null);

		/// <summary>
		/// Used to test the authentication
		/// </summary>
		/// <remarks>
		/// 
		/// </remarks>
		/// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
		/// <param name="echo"> (optional)</param>
		/// <returns>ApiResponse of string</returns>
		ApiResponse<string> CheckoutpaymentgatewayEchoGetWithHttpInfo(string echo = null);
		/// <summary>
		/// Gets a payment information of a particualr request request
		/// </summary>
		/// <remarks>
		/// Gets a payment
		/// </remarks>
		/// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
		/// <param name="body">Payment to find (optional)</param>
		/// <returns>CheckoutPaymentGatewayModelsPaymentResponse</returns>
		CheckoutPaymentGatewayModelsPaymentResponse CheckoutpaymentgatewayGetpaymentGet(Guid? body = null);

		/// <summary>
		/// Gets a payment information of a particualr request request
		/// </summary>
		/// <remarks>
		/// Gets a payment
		/// </remarks>
		/// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
		/// <param name="body">Payment to find (optional)</param>
		/// <returns>ApiResponse of CheckoutPaymentGatewayModelsPaymentResponse</returns>
		ApiResponse<CheckoutPaymentGatewayModelsPaymentResponse> CheckoutpaymentgatewayGetpaymentGetWithHttpInfo(Guid? body = null);
		/// <summary>
		/// Generates a payment request with the gateway
		/// </summary>
		/// <remarks>
		/// Adds a payment
		/// </remarks>
		/// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
		/// <param name="body">Payment to add (optional)</param>
		/// <returns>CheckoutPaymentGatewayModelsPaymentResponse</returns>
		CheckoutPaymentGatewayModelsPaymentResponse CheckoutpaymentgatewayPaymentrequestPost(CheckoutPaymentGatewayModelsPaymentRequest body = null);

		/// <summary>
		/// Generates a payment request with the gateway
		/// </summary>
		/// <remarks>
		/// Adds a payment
		/// </remarks>
		/// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
		/// <param name="body">Payment to add (optional)</param>
		/// <returns>ApiResponse of CheckoutPaymentGatewayModelsPaymentResponse</returns>
		ApiResponse<CheckoutPaymentGatewayModelsPaymentResponse> CheckoutpaymentgatewayPaymentrequestPostWithHttpInfo(CheckoutPaymentGatewayModelsPaymentRequest body = null);
		#endregion Synchronous Operations
		#region Asynchronous Operations
		/// <summary>
		/// Used to test the authentication
		/// </summary>
		/// <remarks>
		/// 
		/// </remarks>
		/// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
		/// <param name="echo"> (optional)</param>
		/// <returns>Task of string</returns>
		System.Threading.Tasks.Task<string> CheckoutpaymentgatewayEchoGetAsync(string echo = null);

		/// <summary>
		/// Used to test the authentication
		/// </summary>
		/// <remarks>
		/// 
		/// </remarks>
		/// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
		/// <param name="echo"> (optional)</param>
		/// <returns>Task of ApiResponse (string)</returns>
		System.Threading.Tasks.Task<ApiResponse<string>> CheckoutpaymentgatewayEchoGetAsyncWithHttpInfo(string echo = null);
		/// <summary>
		/// Gets a payment information of a particualr request request
		/// </summary>
		/// <remarks>
		/// Gets a payment
		/// </remarks>
		/// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
		/// <param name="body">Payment to find (optional)</param>
		/// <returns>Task of CheckoutPaymentGatewayModelsPaymentResponse</returns>
		System.Threading.Tasks.Task<CheckoutPaymentGatewayModelsPaymentResponse> CheckoutpaymentgatewayGetpaymentGetAsync(Guid? body = null);

		/// <summary>
		/// Gets a payment information of a particualr request request
		/// </summary>
		/// <remarks>
		/// Gets a payment
		/// </remarks>
		/// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
		/// <param name="body">Payment to find (optional)</param>
		/// <returns>Task of ApiResponse (CheckoutPaymentGatewayModelsPaymentResponse)</returns>
		System.Threading.Tasks.Task<ApiResponse<CheckoutPaymentGatewayModelsPaymentResponse>> CheckoutpaymentgatewayGetpaymentGetAsyncWithHttpInfo(Guid? body = null);
		/// <summary>
		/// Generates a payment request with the gateway
		/// </summary>
		/// <remarks>
		/// Adds a payment
		/// </remarks>
		/// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
		/// <param name="body">Payment to add (optional)</param>
		/// <returns>Task of CheckoutPaymentGatewayModelsPaymentResponse</returns>
		System.Threading.Tasks.Task<CheckoutPaymentGatewayModelsPaymentResponse> CheckoutpaymentgatewayPaymentrequestPostAsync(CheckoutPaymentGatewayModelsPaymentRequest body = null);

		/// <summary>
		/// Generates a payment request with the gateway
		/// </summary>
		/// <remarks>
		/// Adds a payment
		/// </remarks>
		/// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
		/// <param name="body">Payment to add (optional)</param>
		/// <returns>Task of ApiResponse (CheckoutPaymentGatewayModelsPaymentResponse)</returns>
		System.Threading.Tasks.Task<ApiResponse<CheckoutPaymentGatewayModelsPaymentResponse>> CheckoutpaymentgatewayPaymentrequestPostAsyncWithHttpInfo(CheckoutPaymentGatewayModelsPaymentRequest body = null);
		#endregion Asynchronous Operations
	}
}

/* 
 * Payment Gateway API
 *
 * Payment Gateway API (ASP.NET Core 3.1)
 *
 * OpenAPI spec version: V1
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RestSharp;
using IO.Swagger.Client;
using IO.Swagger.Model;
using IO.Swagger.Interfaces;

namespace IO.Swagger.Api
{
	/// <summary>
	/// Represents a collection of functions to interact with the API endpoints
	/// </summary>
	public partial class PaymentApi : IPaymentApi
	{
		private IO.Swagger.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

		/// <summary>
		/// Initializes a new instance of the <see cref="PaymentApi"/> class.
		/// </summary>
		/// <returns></returns>
		public PaymentApi(String basePath)
		{
			this.Configuration = new IO.Swagger.Client.Configuration { BasePath = basePath };

			ExceptionFactory = IO.Swagger.Client.Configuration.DefaultExceptionFactory;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PaymentApi"/> class
		/// </summary>
		/// <returns></returns>
		public PaymentApi()
		{
			this.Configuration = IO.Swagger.Client.Configuration.Default;

			ExceptionFactory = IO.Swagger.Client.Configuration.DefaultExceptionFactory;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PaymentApi"/> class
		/// using Configuration object
		/// </summary>
		/// <param name="configuration">An instance of Configuration</param>
		/// <returns></returns>
		public PaymentApi(IO.Swagger.Client.Configuration configuration = null)
		{
			if (configuration == null) // use the default one in Configuration
				this.Configuration = IO.Swagger.Client.Configuration.Default;
			else
				this.Configuration = configuration;

			ExceptionFactory = IO.Swagger.Client.Configuration.DefaultExceptionFactory;
		}

		/// <summary>
		/// Gets the base path of the API client.
		/// </summary>
		/// <value>The base path</value>
		public String GetBasePath()
		{
			return this.Configuration.ApiClient.RestClient.BaseUrl.ToString();
		}

		/// <summary>
		/// Gets or sets the configuration object
		/// </summary>
		/// <value>An instance of the Configuration</value>
		public IO.Swagger.Client.Configuration Configuration { get; set; }

		/// <summary>
		/// Provides a factory method hook for the creation of exceptions.
		/// </summary>
		public IO.Swagger.Client.ExceptionFactory ExceptionFactory
		{
			get
			{
				if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
				{
					throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
				}
				return _exceptionFactory;
			}
			set { _exceptionFactory = value; }
		}

		/// <summary>
		/// Gets the default header.
		/// </summary>
		/// <returns>Dictionary of HTTP header</returns>
		[Obsolete("DefaultHeader is deprecated, please use Configuration.DefaultHeader instead.")]
		public IDictionary<String, String> DefaultHeader()
		{
			return new ReadOnlyDictionary<string, string>(this.Configuration.DefaultHeader);
		}

		/// <summary>
		/// Used to test the authentication 
		/// </summary>
		/// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
		/// <param name="echo"> (optional)</param>
		/// <returns>string</returns>
		public string CheckoutpaymentgatewayEchoGet(string echo = null)
		{
			ApiResponse<string> localVarResponse = CheckoutpaymentgatewayEchoGetWithHttpInfo(echo);
			return localVarResponse.Data;
		}

		/// <summary>
		/// Used to test the authentication 
		/// </summary>
		/// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
		/// <param name="echo"> (optional)</param>
		/// <returns>ApiResponse of string</returns>
		public ApiResponse<string> CheckoutpaymentgatewayEchoGetWithHttpInfo(string echo = null)
		{

			var localVarPath = "/checkoutpaymentgateway/Echo";
			var localVarPathParams = new Dictionary<String, String>();
			var localVarQueryParams = new List<KeyValuePair<String, String>>();
			var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
			var localVarFormParams = new Dictionary<String, String>();
			var localVarFileParams = new Dictionary<String, FileParameter>();
			Object localVarPostBody = null;

			// to determine the Content-Type header
			String[] localVarHttpContentTypes = new String[] {
						};
			String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

			// to determine the Accept header
			String[] localVarHttpHeaderAccepts = new String[] {
								"text/plain",
								"application/json",
								"text/json",
								"application/xml",
								"text/xml"
						};
			String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
			if (localVarHttpHeaderAccept != null)
				localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

			if (echo != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "echo", echo)); // query parameter

			// make the HTTP request
			IRestResponse localVarResponse = (IRestResponse)this.Configuration.ApiClient.CallApi(localVarPath,
					Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
					localVarPathParams, localVarHttpContentType);

			int localVarStatusCode = (int)localVarResponse.StatusCode;

			if (ExceptionFactory != null)
			{
				Exception exception = ExceptionFactory("CheckoutpaymentgatewayEchoGet", localVarResponse);
				if (exception != null) throw exception;
			}

			return new ApiResponse<string>(localVarStatusCode,
					localVarResponse.Headers.ToDictionary(x => x.Name, x => string.Join(",", x.Value)),
					(string)this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(string)));
		}

		/// <summary>
		/// Used to test the authentication 
		/// </summary>
		/// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
		/// <param name="echo"> (optional)</param>
		/// <returns>Task of string</returns>
		public async System.Threading.Tasks.Task<string> CheckoutpaymentgatewayEchoGetAsync(string echo = null)
		{
			ApiResponse<string> localVarResponse = await CheckoutpaymentgatewayEchoGetAsyncWithHttpInfo(echo);
			return localVarResponse.Data;

		}

		/// <summary>
		/// Used to test the authentication 
		/// </summary>
		/// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
		/// <param name="echo"> (optional)</param>
		/// <returns>Task of ApiResponse (string)</returns>
		public async System.Threading.Tasks.Task<ApiResponse<string>> CheckoutpaymentgatewayEchoGetAsyncWithHttpInfo(string echo = null)
		{

			var localVarPath = "/checkoutpaymentgateway/Echo";
			var localVarPathParams = new Dictionary<String, String>();
			var localVarQueryParams = new List<KeyValuePair<String, String>>();
			var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
			var localVarFormParams = new Dictionary<String, String>();
			var localVarFileParams = new Dictionary<String, FileParameter>();
			Object localVarPostBody = null;

			// to determine the Content-Type header
			String[] localVarHttpContentTypes = new String[] {
						};
			String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

			// to determine the Accept header
			String[] localVarHttpHeaderAccepts = new String[] {
								"text/plain",
								"application/json",
								"text/json",
								"application/xml",
								"text/xml"
						};
			String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
			if (localVarHttpHeaderAccept != null)
				localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

			if (echo != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "echo", echo)); // query parameter

			// make the HTTP request
			IRestResponse localVarResponse = (IRestResponse)await this.Configuration.ApiClient.CallApiAsync(localVarPath,
					Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
					localVarPathParams, localVarHttpContentType);

			int localVarStatusCode = (int)localVarResponse.StatusCode;

			if (ExceptionFactory != null)
			{
				Exception exception = ExceptionFactory("CheckoutpaymentgatewayEchoGet", localVarResponse);
				if (exception != null) throw exception;
			}

			return new ApiResponse<string>(localVarStatusCode,
					localVarResponse.Headers.ToDictionary(x => x.Name, x => string.Join(",", x.Value)),
					(string)this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(string)));
		}

		/// <summary>
		/// Gets a payment information of a particualr request request Gets a payment
		/// </summary>
		/// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
		/// <param name="body">Payment to find (optional)</param>
		/// <returns>CheckoutPaymentGatewayModelsPaymentResponse</returns>
		public CheckoutPaymentGatewayModelsPaymentResponse CheckoutpaymentgatewayGetpaymentGet(Guid? body = null)
		{
			ApiResponse<CheckoutPaymentGatewayModelsPaymentResponse> localVarResponse = CheckoutpaymentgatewayGetpaymentGetWithHttpInfo(body);
			return localVarResponse.Data;
		}

		/// <summary>
		/// Gets a payment information of a particualr request request Gets a payment
		/// </summary>
		/// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
		/// <param name="body">Payment to find (optional)</param>
		/// <returns>ApiResponse of CheckoutPaymentGatewayModelsPaymentResponse</returns>
		public ApiResponse<CheckoutPaymentGatewayModelsPaymentResponse> CheckoutpaymentgatewayGetpaymentGetWithHttpInfo(Guid? body = null)
		{

			var localVarPath = "/checkoutpaymentgateway/getpayment";
			var localVarPathParams = new Dictionary<String, String>();
			var localVarQueryParams = new List<KeyValuePair<String, String>>();
			var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
			var localVarFormParams = new Dictionary<String, String>();
			var localVarFileParams = new Dictionary<String, FileParameter>();
			Object localVarPostBody = null;

			// to determine the Content-Type header
			String[] localVarHttpContentTypes = new String[] {
						};
			String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

			// to determine the Accept header
			String[] localVarHttpHeaderAccepts = new String[] {
								"text/plain",
								"application/json",
								"text/json",
								"application/xml",
								"text/xml"
						};
			String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
			if (localVarHttpHeaderAccept != null)
				localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

			if (body != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "body", body)); // query parameter

			// make the HTTP request
			IRestResponse localVarResponse = (IRestResponse)this.Configuration.ApiClient.CallApi(localVarPath,
					Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
					localVarPathParams, localVarHttpContentType);

			int localVarStatusCode = (int)localVarResponse.StatusCode;

			if (ExceptionFactory != null)
			{
				Exception exception = ExceptionFactory("CheckoutpaymentgatewayGetpaymentGet", localVarResponse);
				if (exception != null) throw exception;
			}

			return new ApiResponse<CheckoutPaymentGatewayModelsPaymentResponse>(localVarStatusCode,
					localVarResponse.Headers.ToDictionary(x => x.Name, x => string.Join(",", x.Value)),
					(CheckoutPaymentGatewayModelsPaymentResponse)this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(CheckoutPaymentGatewayModelsPaymentResponse)));
		}

		/// <summary>
		/// Gets a payment information of a particualr request request Gets a payment
		/// </summary>
		/// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
		/// <param name="body">Payment to find (optional)</param>
		/// <returns>Task of CheckoutPaymentGatewayModelsPaymentResponse</returns>
		public async System.Threading.Tasks.Task<CheckoutPaymentGatewayModelsPaymentResponse> CheckoutpaymentgatewayGetpaymentGetAsync(Guid? body = null)
		{
			ApiResponse<CheckoutPaymentGatewayModelsPaymentResponse> localVarResponse = await CheckoutpaymentgatewayGetpaymentGetAsyncWithHttpInfo(body);
			return localVarResponse.Data;

		}

		/// <summary>
		/// Gets a payment information of a particualr request request Gets a payment
		/// </summary>
		/// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
		/// <param name="body">Payment to find (optional)</param>
		/// <returns>Task of ApiResponse (CheckoutPaymentGatewayModelsPaymentResponse)</returns>
		public async System.Threading.Tasks.Task<ApiResponse<CheckoutPaymentGatewayModelsPaymentResponse>> CheckoutpaymentgatewayGetpaymentGetAsyncWithHttpInfo(Guid? body = null)
		{

			var localVarPath = "/checkoutpaymentgateway/getpayment";
			var localVarPathParams = new Dictionary<String, String>();
			var localVarQueryParams = new List<KeyValuePair<String, String>>();
			var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
			var localVarFormParams = new Dictionary<String, String>();
			var localVarFileParams = new Dictionary<String, FileParameter>();
			Object localVarPostBody = null;

			// to determine the Content-Type header
			String[] localVarHttpContentTypes = new String[] {
						};
			String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

			// to determine the Accept header
			String[] localVarHttpHeaderAccepts = new String[] {
								"text/plain",
								"application/json",
								"text/json",
								"application/xml",
								"text/xml"
						};
			String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
			if (localVarHttpHeaderAccept != null)
				localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

			if (body != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "body", body)); // query parameter

			// make the HTTP request
			IRestResponse localVarResponse = (IRestResponse)await this.Configuration.ApiClient.CallApiAsync(localVarPath,
					Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
					localVarPathParams, localVarHttpContentType);

			int localVarStatusCode = (int)localVarResponse.StatusCode;

			if (ExceptionFactory != null)
			{
				Exception exception = ExceptionFactory("CheckoutpaymentgatewayGetpaymentGet", localVarResponse);
				if (exception != null) throw exception;
			}

			return new ApiResponse<CheckoutPaymentGatewayModelsPaymentResponse>(localVarStatusCode,
					localVarResponse.Headers.ToDictionary(x => x.Name, x => string.Join(",", x.Value)),
					(CheckoutPaymentGatewayModelsPaymentResponse)this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(CheckoutPaymentGatewayModelsPaymentResponse)));
		}

		/// <summary>
		/// Generates a payment request with the gateway Adds a payment
		/// </summary>
		/// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
		/// <param name="body">Payment to add (optional)</param>
		/// <returns>CheckoutPaymentGatewayModelsPaymentResponse</returns>
		public CheckoutPaymentGatewayModelsPaymentResponse CheckoutpaymentgatewayPaymentrequestPost(CheckoutPaymentGatewayModelsPaymentRequest body = null)
		{
			ApiResponse<CheckoutPaymentGatewayModelsPaymentResponse> localVarResponse = CheckoutpaymentgatewayPaymentrequestPostWithHttpInfo(body);
			return localVarResponse.Data;
		}

		/// <summary>
		/// Generates a payment request with the gateway Adds a payment
		/// </summary>
		/// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
		/// <param name="body">Payment to add (optional)</param>
		/// <returns>ApiResponse of CheckoutPaymentGatewayModelsPaymentResponse</returns>
		public ApiResponse<CheckoutPaymentGatewayModelsPaymentResponse> CheckoutpaymentgatewayPaymentrequestPostWithHttpInfo(CheckoutPaymentGatewayModelsPaymentRequest body = null)
		{

			var localVarPath = "/checkoutpaymentgateway/paymentrequest";
			var localVarPathParams = new Dictionary<String, String>();
			var localVarQueryParams = new List<KeyValuePair<String, String>>();
			var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
			var localVarFormParams = new Dictionary<String, String>();
			var localVarFileParams = new Dictionary<String, FileParameter>();
			Object localVarPostBody = null;

			// to determine the Content-Type header
			String[] localVarHttpContentTypes = new String[] {
								"application/json-patch+json",
								"application/json",
								"text/json",
								"application/_*+json",
								"application/xml",
								"text/xml",
								"application/_*+xml"
						};
			String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

			// to determine the Accept header
			String[] localVarHttpHeaderAccepts = new String[] {
								"text/plain",
								"application/json",
								"text/json",
								"application/xml",
								"text/xml"
						};
			String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
			if (localVarHttpHeaderAccept != null)
				localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

			if (body != null && body.GetType() != typeof(byte[]))
			{
				localVarPostBody = this.Configuration.ApiClient.Serialize(body); // http body (model) parameter
			}
			else
			{
				localVarPostBody = body; // byte array
			}

			// make the HTTP request
			IRestResponse localVarResponse = (IRestResponse)this.Configuration.ApiClient.CallApi(localVarPath,
					Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
					localVarPathParams, localVarHttpContentType);

			int localVarStatusCode = (int)localVarResponse.StatusCode;

			if (ExceptionFactory != null)
			{
				Exception exception = ExceptionFactory("CheckoutpaymentgatewayPaymentrequestPost", localVarResponse);
				if (exception != null) throw exception;
			}

			return new ApiResponse<CheckoutPaymentGatewayModelsPaymentResponse>(localVarStatusCode,
					localVarResponse.Headers.ToDictionary(x => x.Name, x => string.Join(",", x.Value)),
					(CheckoutPaymentGatewayModelsPaymentResponse)this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(CheckoutPaymentGatewayModelsPaymentResponse)));
		}

		/// <summary>
		/// Generates a payment request with the gateway Adds a payment
		/// </summary>
		/// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
		/// <param name="body">Payment to add (optional)</param>
		/// <returns>Task of CheckoutPaymentGatewayModelsPaymentResponse</returns>
		public async System.Threading.Tasks.Task<CheckoutPaymentGatewayModelsPaymentResponse> CheckoutpaymentgatewayPaymentrequestPostAsync(CheckoutPaymentGatewayModelsPaymentRequest body = null)
		{
			ApiResponse<CheckoutPaymentGatewayModelsPaymentResponse> localVarResponse = await CheckoutpaymentgatewayPaymentrequestPostAsyncWithHttpInfo(body);
			return localVarResponse.Data;

		}

		/// <summary>
		/// Generates a payment request with the gateway Adds a payment
		/// </summary>
		/// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
		/// <param name="body">Payment to add (optional)</param>
		/// <returns>Task of ApiResponse (CheckoutPaymentGatewayModelsPaymentResponse)</returns>
		public async System.Threading.Tasks.Task<ApiResponse<CheckoutPaymentGatewayModelsPaymentResponse>> CheckoutpaymentgatewayPaymentrequestPostAsyncWithHttpInfo(CheckoutPaymentGatewayModelsPaymentRequest body = null)
		{

			var localVarPath = "/checkoutpaymentgateway/paymentrequest";
			var localVarPathParams = new Dictionary<String, String>();
			var localVarQueryParams = new List<KeyValuePair<String, String>>();
			var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
			var localVarFormParams = new Dictionary<String, String>();
			var localVarFileParams = new Dictionary<String, FileParameter>();
			Object localVarPostBody = null;

			// to determine the Content-Type header
			String[] localVarHttpContentTypes = new String[] {
								"application/json-patch+json",
								"application/json",
								"text/json",
								"application/_*+json",
								"application/xml",
								"text/xml",
								"application/_*+xml"
						};
			String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

			// to determine the Accept header
			String[] localVarHttpHeaderAccepts = new String[] {
								"text/plain",
								"application/json",
								"text/json",
								"application/xml",
								"text/xml"
						};
			String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
			if (localVarHttpHeaderAccept != null)
				localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

			if (body != null && body.GetType() != typeof(byte[]))
			{
				localVarPostBody = this.Configuration.ApiClient.Serialize(body); // http body (model) parameter
			}
			else
			{
				localVarPostBody = body; // byte array
			}

			// make the HTTP request
			IRestResponse localVarResponse = (IRestResponse)await this.Configuration.ApiClient.CallApiAsync(localVarPath,
					Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
					localVarPathParams, localVarHttpContentType);

			int localVarStatusCode = (int)localVarResponse.StatusCode;

			if (ExceptionFactory != null)
			{
				Exception exception = ExceptionFactory("CheckoutpaymentgatewayPaymentrequestPost", localVarResponse);
				if (exception != null) throw exception;
			}

			return new ApiResponse<CheckoutPaymentGatewayModelsPaymentResponse>(localVarStatusCode,
					localVarResponse.Headers.ToDictionary(x => x.Name, x => string.Join(",", x.Value)),
					(CheckoutPaymentGatewayModelsPaymentResponse)this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(CheckoutPaymentGatewayModelsPaymentResponse)));
		}

	}
}
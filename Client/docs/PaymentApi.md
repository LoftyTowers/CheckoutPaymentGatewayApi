# IO.Swagger.Api.PaymentApi

All URIs are relative to *https://virtserver.swaggerhub.com/BrambyPerspective/CheckoutPaymentGatewayAPI/V1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CheckoutpaymentgatewayEchoGet**](PaymentApi.md#checkoutpaymentgatewayechoget) | **GET** /checkoutpaymentgateway/Echo | Used to test the authentication
[**CheckoutpaymentgatewayGetpaymentGet**](PaymentApi.md#checkoutpaymentgatewaygetpaymentget) | **GET** /checkoutpaymentgateway/getpayment | Gets a payment information of a particualr request request
[**CheckoutpaymentgatewayPaymentrequestPost**](PaymentApi.md#checkoutpaymentgatewaypaymentrequestpost) | **POST** /checkoutpaymentgateway/paymentrequest | Generates a payment request with the gateway

<a name="checkoutpaymentgatewayechoget"></a>
# **CheckoutpaymentgatewayEchoGet**
> string CheckoutpaymentgatewayEchoGet (string echo = null)

Used to test the authentication

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CheckoutpaymentgatewayEchoGetExample
    {
        public void main()
        {
            var apiInstance = new PaymentApi();
            var echo = echo_example;  // string |  (optional) 

            try
            {
                // Used to test the authentication
                string result = apiInstance.CheckoutpaymentgatewayEchoGet(echo);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PaymentApi.CheckoutpaymentgatewayEchoGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **echo** | **string**|  | [optional] 

### Return type

**string**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json, application/xml, text/xml

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="checkoutpaymentgatewaygetpaymentget"></a>
# **CheckoutpaymentgatewayGetpaymentGet**
> CheckoutPaymentGatewayModelsPaymentResponse CheckoutpaymentgatewayGetpaymentGet (Guid? body = null)

Gets a payment information of a particualr request request

Gets a payment

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CheckoutpaymentgatewayGetpaymentGetExample
    {
        public void main()
        {
            var apiInstance = new PaymentApi();
            var body = new Guid?(); // Guid? | Payment to find (optional) 

            try
            {
                // Gets a payment information of a particualr request request
                CheckoutPaymentGatewayModelsPaymentResponse result = apiInstance.CheckoutpaymentgatewayGetpaymentGet(body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PaymentApi.CheckoutpaymentgatewayGetpaymentGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**Guid?**](Guid?.md)| Payment to find | [optional] 

### Return type

[**CheckoutPaymentGatewayModelsPaymentResponse**](CheckoutPaymentGatewayModelsPaymentResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json, application/xml, text/xml

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="checkoutpaymentgatewaypaymentrequestpost"></a>
# **CheckoutpaymentgatewayPaymentrequestPost**
> CheckoutPaymentGatewayModelsPaymentResponse CheckoutpaymentgatewayPaymentrequestPost (CheckoutPaymentGatewayModelsPaymentRequest body = null)

Generates a payment request with the gateway

Adds a payment

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CheckoutpaymentgatewayPaymentrequestPostExample
    {
        public void main()
        {
            var apiInstance = new PaymentApi();
            var body = new CheckoutPaymentGatewayModelsPaymentRequest(); // CheckoutPaymentGatewayModelsPaymentRequest | Payment to add (optional) 

            try
            {
                // Generates a payment request with the gateway
                CheckoutPaymentGatewayModelsPaymentResponse result = apiInstance.CheckoutpaymentgatewayPaymentrequestPost(body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PaymentApi.CheckoutpaymentgatewayPaymentrequestPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**CheckoutPaymentGatewayModelsPaymentRequest**](CheckoutPaymentGatewayModelsPaymentRequest.md)| Payment to add | [optional] 

### Return type

[**CheckoutPaymentGatewayModelsPaymentResponse**](CheckoutPaymentGatewayModelsPaymentResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json, application/xml, text/xml, application/_*+xml
 - **Accept**: text/plain, application/json, text/json, application/xml, text/xml

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# IO.Swagger.Model.CheckoutPaymentGatewayModelsPaymentResponse
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **Guid?** | Unique Identifier of the payment | 
**CurrencyCode** | **string** | The currency code the transaction is made in | 
**Amount** | **double?** | The amount of the transaction | 
**Cvc** | **int?** | The cvc customers card | 
**CardNumber** | **long?** | The CardNumber for the transaction | 
**FullName** | **string** | The FullName of the customer as shown on the card | 
**CardExpiryDate** | **DateTime?** | The expiry date of the customers card | 
**RequestDate** | **DateTime?** | The date the transaction was initilised | 
**IsSuccessful** | **bool?** | Descibes whether the payment was succesful or not | [default to false]
**Message** | **string** | Shows any further information if required (i.e. useful error messages) | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


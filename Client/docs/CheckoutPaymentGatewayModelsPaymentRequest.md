# IO.Swagger.Model.CheckoutPaymentGatewayModelsPaymentRequest
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **Guid?** | Unique Identifier of the payment | 
**CurrencyCode** | **string** | The currency code the transaction is made in | 
**Amount** | **double?** | The amount of the transaction | 
**Cvc** | **int?** | The cvc customers card | 
**CardNumber** | **long?** | The CardNumber for the transaction | 
**FullName** | **string** | The FullName of the customer as shown on the card | 
**DateOfBirth** | **string** | The date of birth of the card holder for authentication | 
**CardExpiryDate** | **DateTime?** | The expiry date of the customers card | 
**RequestDate** | **DateTime?** | The date the transaction was initilised | 
**SendingBankName** | **string** | NAme of the bank the payment is made from | 
**RecievingBankName** | **string** | Name of the bank the payment is sent to | 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


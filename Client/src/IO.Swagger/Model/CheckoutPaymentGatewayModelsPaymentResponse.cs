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
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = IO.Swagger.Client.SwaggerDateConverter;

namespace IO.Swagger.Model
{
    /// <summary>
    /// This is the payment response contract for the merchant
    /// </summary>
    [DataContract]
        public partial class CheckoutPaymentGatewayModelsPaymentResponse :  IEquatable<CheckoutPaymentGatewayModelsPaymentResponse>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutPaymentGatewayModelsPaymentResponse" /> class.
        /// </summary>
        /// <param name="id">Unique Identifier of the payment (required).</param>
        /// <param name="currencyCode">The currency code the transaction is made in (required).</param>
        /// <param name="amount">The amount of the transaction (required).</param>
        /// <param name="cvc">The cvc customers card (required).</param>
        /// <param name="cardNumber">The CardNumber for the transaction (required).</param>
        /// <param name="fullName">The FullName of the customer as shown on the card (required).</param>
        /// <param name="cardExpiryDate">The expiry date of the customers card (required).</param>
        /// <param name="requestDate">The date the transaction was initilised (required).</param>
        /// <param name="isSuccessful">Descibes whether the payment was succesful or not (required) (default to false).</param>
        /// <param name="message">Shows any further information if required (i.e. useful error messages).</param>
        public CheckoutPaymentGatewayModelsPaymentResponse(Guid? id = default(Guid?), string currencyCode = default(string), double? amount = default(double?), int? cvc = default(int?), long? cardNumber = default(long?), string fullName = default(string), DateTime? cardExpiryDate = default(DateTime?), DateTime? requestDate = default(DateTime?), bool? isSuccessful = false, string message = default(string))
        {
            // to ensure "id" is required (not null)
            if (id == null)
            {
                throw new InvalidDataException("id is a required property for CheckoutPaymentGatewayModelsPaymentResponse and cannot be null");
            }
            else
            {
                this.Id = id;
            }
            // to ensure "currencyCode" is required (not null)
            if (currencyCode == null)
            {
                throw new InvalidDataException("currencyCode is a required property for CheckoutPaymentGatewayModelsPaymentResponse and cannot be null");
            }
            else
            {
                this.CurrencyCode = currencyCode;
            }
            // to ensure "amount" is required (not null)
            if (amount == null)
            {
                throw new InvalidDataException("amount is a required property for CheckoutPaymentGatewayModelsPaymentResponse and cannot be null");
            }
            else
            {
                this.Amount = amount;
            }
            // to ensure "cvc" is required (not null)
            if (cvc == null)
            {
                throw new InvalidDataException("cvc is a required property for CheckoutPaymentGatewayModelsPaymentResponse and cannot be null");
            }
            else
            {
                this.Cvc = cvc;
            }
            // to ensure "cardNumber" is required (not null)
            if (cardNumber == null)
            {
                throw new InvalidDataException("cardNumber is a required property for CheckoutPaymentGatewayModelsPaymentResponse and cannot be null");
            }
            else
            {
                this.CardNumber = cardNumber;
            }
            // to ensure "fullName" is required (not null)
            if (fullName == null)
            {
                throw new InvalidDataException("fullName is a required property for CheckoutPaymentGatewayModelsPaymentResponse and cannot be null");
            }
            else
            {
                this.FullName = fullName;
            }
            // to ensure "cardExpiryDate" is required (not null)
            if (cardExpiryDate == null)
            {
                throw new InvalidDataException("cardExpiryDate is a required property for CheckoutPaymentGatewayModelsPaymentResponse and cannot be null");
            }
            else
            {
                this.CardExpiryDate = cardExpiryDate;
            }
            // to ensure "requestDate" is required (not null)
            if (requestDate == null)
            {
                throw new InvalidDataException("requestDate is a required property for CheckoutPaymentGatewayModelsPaymentResponse and cannot be null");
            }
            else
            {
                this.RequestDate = requestDate;
            }
            // to ensure "isSuccessful" is required (not null)
            if (isSuccessful == null)
            {
                throw new InvalidDataException("isSuccessful is a required property for CheckoutPaymentGatewayModelsPaymentResponse and cannot be null");
            }
            else
            {
                this.IsSuccessful = isSuccessful;
            }
            this.Message = message;
        }
        
        /// <summary>
        /// Unique Identifier of the payment
        /// </summary>
        /// <value>Unique Identifier of the payment</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public Guid? Id { get; set; }

        /// <summary>
        /// The currency code the transaction is made in
        /// </summary>
        /// <value>The currency code the transaction is made in</value>
        [DataMember(Name="currencyCode", EmitDefaultValue=false)]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// The amount of the transaction
        /// </summary>
        /// <value>The amount of the transaction</value>
        [DataMember(Name="amount", EmitDefaultValue=false)]
        public double? Amount { get; set; }

        /// <summary>
        /// The cvc customers card
        /// </summary>
        /// <value>The cvc customers card</value>
        [DataMember(Name="cvc", EmitDefaultValue=false)]
        public int? Cvc { get; set; }

        /// <summary>
        /// The CardNumber for the transaction
        /// </summary>
        /// <value>The CardNumber for the transaction</value>
        [DataMember(Name="cardNumber", EmitDefaultValue=false)]
        public long? CardNumber { get; set; }

        /// <summary>
        /// The FullName of the customer as shown on the card
        /// </summary>
        /// <value>The FullName of the customer as shown on the card</value>
        [DataMember(Name="fullName", EmitDefaultValue=false)]
        public string FullName { get; set; }

        /// <summary>
        /// The expiry date of the customers card
        /// </summary>
        /// <value>The expiry date of the customers card</value>
        [DataMember(Name="cardExpiryDate", EmitDefaultValue=false)]
        public DateTime? CardExpiryDate { get; set; }

        /// <summary>
        /// The date the transaction was initilised
        /// </summary>
        /// <value>The date the transaction was initilised</value>
        [DataMember(Name="requestDate", EmitDefaultValue=false)]
        public DateTime? RequestDate { get; set; }

        /// <summary>
        /// Descibes whether the payment was succesful or not
        /// </summary>
        /// <value>Descibes whether the payment was succesful or not</value>
        [DataMember(Name="isSuccessful", EmitDefaultValue=false)]
        public bool? IsSuccessful { get; set; }

        /// <summary>
        /// Shows any further information if required (i.e. useful error messages)
        /// </summary>
        /// <value>Shows any further information if required (i.e. useful error messages)</value>
        [DataMember(Name="message", EmitDefaultValue=false)]
        public string Message { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CheckoutPaymentGatewayModelsPaymentResponse {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  CurrencyCode: ").Append(CurrencyCode).Append("\n");
            sb.Append("  Amount: ").Append(Amount).Append("\n");
            sb.Append("  Cvc: ").Append(Cvc).Append("\n");
            sb.Append("  CardNumber: ").Append(CardNumber).Append("\n");
            sb.Append("  FullName: ").Append(FullName).Append("\n");
            sb.Append("  CardExpiryDate: ").Append(CardExpiryDate).Append("\n");
            sb.Append("  RequestDate: ").Append(RequestDate).Append("\n");
            sb.Append("  IsSuccessful: ").Append(IsSuccessful).Append("\n");
            sb.Append("  Message: ").Append(Message).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as CheckoutPaymentGatewayModelsPaymentResponse);
        }

        /// <summary>
        /// Returns true if CheckoutPaymentGatewayModelsPaymentResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of CheckoutPaymentGatewayModelsPaymentResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CheckoutPaymentGatewayModelsPaymentResponse input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.CurrencyCode == input.CurrencyCode ||
                    (this.CurrencyCode != null &&
                    this.CurrencyCode.Equals(input.CurrencyCode))
                ) && 
                (
                    this.Amount == input.Amount ||
                    (this.Amount != null &&
                    this.Amount.Equals(input.Amount))
                ) && 
                (
                    this.Cvc == input.Cvc ||
                    (this.Cvc != null &&
                    this.Cvc.Equals(input.Cvc))
                ) && 
                (
                    this.CardNumber == input.CardNumber ||
                    (this.CardNumber != null &&
                    this.CardNumber.Equals(input.CardNumber))
                ) && 
                (
                    this.FullName == input.FullName ||
                    (this.FullName != null &&
                    this.FullName.Equals(input.FullName))
                ) && 
                (
                    this.CardExpiryDate == input.CardExpiryDate ||
                    (this.CardExpiryDate != null &&
                    this.CardExpiryDate.Equals(input.CardExpiryDate))
                ) && 
                (
                    this.RequestDate == input.RequestDate ||
                    (this.RequestDate != null &&
                    this.RequestDate.Equals(input.RequestDate))
                ) && 
                (
                    this.IsSuccessful == input.IsSuccessful ||
                    (this.IsSuccessful != null &&
                    this.IsSuccessful.Equals(input.IsSuccessful))
                ) && 
                (
                    this.Message == input.Message ||
                    (this.Message != null &&
                    this.Message.Equals(input.Message))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.CurrencyCode != null)
                    hashCode = hashCode * 59 + this.CurrencyCode.GetHashCode();
                if (this.Amount != null)
                    hashCode = hashCode * 59 + this.Amount.GetHashCode();
                if (this.Cvc != null)
                    hashCode = hashCode * 59 + this.Cvc.GetHashCode();
                if (this.CardNumber != null)
                    hashCode = hashCode * 59 + this.CardNumber.GetHashCode();
                if (this.FullName != null)
                    hashCode = hashCode * 59 + this.FullName.GetHashCode();
                if (this.CardExpiryDate != null)
                    hashCode = hashCode * 59 + this.CardExpiryDate.GetHashCode();
                if (this.RequestDate != null)
                    hashCode = hashCode * 59 + this.RequestDate.GetHashCode();
                if (this.IsSuccessful != null)
                    hashCode = hashCode * 59 + this.IsSuccessful.GetHashCode();
                if (this.Message != null)
                    hashCode = hashCode * 59 + this.Message.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
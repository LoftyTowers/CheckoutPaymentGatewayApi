# IO.Swagger - ASP.NET Core 3.1

Validates payment requests, stores card information, forwards payment requests and accepts responses from the acquiring bank.

## Run

Linux/OS X:

```
sh build.sh
```

Windows:

```
build.bat
```

## Run in Docker

```
cd CheckoutPayementGatewayApi\CheckoutPaymentGateway
docker build -t CheckoutPaymentGateway .
docker run -p 5000:5000 CheckoutPaymentGateway
```

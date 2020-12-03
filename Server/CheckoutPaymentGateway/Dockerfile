FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app
EXPOSE 443

COPY . ./
RUN dotnet publish CheckoutPaymentGateway -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "CheckoutPaymentGateway.dll", "--environment=Production"]
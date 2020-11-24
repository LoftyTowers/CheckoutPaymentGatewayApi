##See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
#WORKDIR /app
#EXPOSE 80
#
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
#WORKDIR /src
#COPY ["CheckoutPaymentGateway/CheckoutPaymentGateway.csproj", "CheckoutPaymentGateway/"]
#COPY ["PaymentGatewayService/PaymentGatewayService.csproj", "PaymentGatewayService/"]
#COPY ["Common/Common.csproj", "Common/"]
#COPY ["Repositories/Repositories.csproj", "Repositories/"]
#RUN dotnet restore "CheckoutPaymentGateway/CheckoutPaymentGateway.csproj"
#COPY . .
#WORKDIR "/src/CheckoutPaymentGateway"
#RUN dotnet build "CheckoutPaymentGateway.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "CheckoutPaymentGateway.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "CheckoutPaymentGateway.dll"]
#

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
EXPOSE 443

COPY . ./
RUN dotnet publish CheckoutPaymentGateway -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "CheckoutPaymentGateway.dll", "--environment=Development"]
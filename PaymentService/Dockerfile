FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-payment
WORKDIR /source
COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out/payment
FROM mcr.microsoft.com/dotnet/core/aspnet
WORKDIR /app
COPY --from=build-payment /source/out/payment ./

ENTRYPOINT ["dotnet", "PaymentService.dll"]

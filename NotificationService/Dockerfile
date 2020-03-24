FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-notification
WORKDIR /source
COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out/notification
FROM mcr.microsoft.com/dotnet/core/aspnet
WORKDIR /app
COPY --from=build-notification /source/out/notification ./

ENTRYPOINT ["dotnet", "NotificationService.dll"]

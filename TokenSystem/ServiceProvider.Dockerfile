FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY "ServiceProviderService/ServiceProvider.API/" "ServiceProvider.API/"
COPY "ServiceProviderService/ServiceProvider.Application/" "ServiceProvider.Application/"
COPY "ServiceProviderService/ServiceProvider.Domain/" "ServiceProvider.Domain/"
COPY "ServiceProviderService/ServiceProvider.Infrastructure/" "ServiceProvider.Infrastructure/"

# Copy TokenIssuanceService.Domain project
COPY "TokenIssuanceService/TokenIssuanceService.Domain/" "/TokenIssuanceService/TokenIssuanceService.Domain/"

RUN dotnet restore "ServiceProvider.API/ServiceProvider.API.csproj"
COPY . .
WORKDIR "/src/ServiceProvider.API/"
RUN dotnet build "ServiceProvider.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ServiceProvider.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServiceProvider.API.dll"]
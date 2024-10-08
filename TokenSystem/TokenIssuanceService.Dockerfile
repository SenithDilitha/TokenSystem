FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY "TokenIssuanceService/TokenIssuanceService.API/" "TokenIssuanceService.API/"
COPY "TokenIssuanceService/TokenIssuanceService.Application/" "TokenIssuanceService.Application/"
COPY "TokenIssuanceService/TokenIssuanceService.Domain/" "TokenIssuanceService.Domain/"
COPY "TokenIssuanceService/TokenIssuanceService.Infrastructure/" "TokenIssuanceService.Infrastructure/"

RUN dotnet restore "TokenIssuanceService.API/TokenIssuanceService.API.csproj"
COPY . .
WORKDIR "/src/TokenIssuanceService.API/"
RUN dotnet build "TokenIssuanceService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TokenIssuanceService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TokenIssuanceService.API.dll"]
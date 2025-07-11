#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["api.net8.Api/api_net9.csproj", "api.net9.Api/"]
COPY ["api_.net8.Common/api_.net9.Common.csproj", "api_.net9.Common/"]
COPY ["api_net8.Application/api_net9.Application.csproj", "api_net9.Application/"]
COPY ["api_net8.Domain/api_net9.Domain.csproj", "api_net9.Domain/"]
COPY ["api_net8.Infrastructure/api_net9.Infrastructure.csproj", "api_net9.Infrastructure/"]
RUN dotnet restore "api.net9.Api/api_net9.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "api.net8.Api/api_net9.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "api.net8.Api/api_net9.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "api_net9.dll"]

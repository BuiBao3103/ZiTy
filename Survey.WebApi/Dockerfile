#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Survey.WebApi/Survey.WebApi.csproj", "Survey.WebApi/"]
COPY ["Survey.Application/Survey.Application.csproj", "Survey.Application/"]
COPY ["Survey.Domain/Survey.Domain.csproj", "Survey.Domain/"]
COPY ["Survey.Infrastructure/Survey.Infrastructure.csproj", "Survey.Infrastructure/"]
RUN dotnet restore "./Survey.WebApi/Survey.WebApi.csproj"
COPY . .
WORKDIR "/src/Survey.WebApi"
RUN dotnet build "./Survey.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Survey.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Survey.WebApi.dll"]
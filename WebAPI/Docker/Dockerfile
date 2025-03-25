# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["WebAPI/WebAPI.csproj", "WebAPI/"]
COPY ["API.Application/API.Application.csproj", "API.Application/"]
COPY ["API.Domain/API.Domain.csproj", "API.Domain/"]
COPY ["API.Infrastructure/API.Infrastructure.csproj", "API.Infrastructure/"]

RUN dotnet restore "WebAPI/WebAPI.csproj"

COPY . .

WORKDIR "/src/WebAPI"
RUN dotnet publish "WebAPI.csproj" -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "WebAPI.dll"]

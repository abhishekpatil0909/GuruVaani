# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["src/Guruvani.Api/Guruvani.Api.csproj", "src/Guruvani.Api/"]
COPY ["src/Guruvani.Infrastructure/Guruvani.Infrastructure.csproj", "src/Guruvani.Infrastructure/"]
COPY ["src/Guruvani.Application/Guruvani.Application.csproj", "src/Guruvani.Application/"]
COPY ["src/Guruvani.Domain/Guruvani.Domain.csproj", "src/Guruvani.Domain/"]
RUN dotnet restore "src/Guruvani.Api/Guruvani.Api.csproj"

# Copy everything else and publish
COPY . .
WORKDIR "/src/src/Guruvani.Api"
RUN dotnet publish "Guruvani.Api.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "Guruvani.Api.dll"]

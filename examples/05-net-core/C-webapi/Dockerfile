# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./MyWebApi/MyWebApi.csproj" --disable-parallel
RUN dotnet publish "./MyWebApi/MyWebApi.csproj" -c release -o /app --no-restore

# Serve Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000
ENTRYPOINT ["dotnet","MyWebApi.dll"]

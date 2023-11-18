# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./MyConsole/MyConsole.csproj" --disable-parallel
RUN dotnet publish "./MyConsole/MyConsole.csproj" -c release -o /app --no-restore

# Serve Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet","MyConsole.dll"]

FROM mcr.microsoft.com/dotnet/sdk:8.0

COPY publish /app
WORKDIR /app

EXPOSE 5000
ENTRYPOINT ["dotnet","MyWebMVC.dll"]

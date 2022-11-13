FROM mcr.microsoft.com/dotnet/sdk:6.0-focal

COPY publish /app
WORKDIR /app

EXPOSE 6000
ENTRYPOINT ["dotnet","MyWebMVC.dll"]

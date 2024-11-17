# Manual Model (Redis not run inside docker)

## For windows
1. Download redis https://github.com/microsoftarchive/redis/tags
2. Extract portable the redis app
3. Open terminal run <code>Redis-x64-3.2.100.exe</code> 

## Run the application
1. Set the appropriate redis connection string in the <code>appsettings.json</code>
    ```
    "Redis": "localhost:6379"
    ```
2. Run the example app.

   
# Deploy to Docker
1. Run docker compose build
```
docker-compose build
```

2. Build container
```
docker-compose up
```

3. Monitor the log console in terminal

# Notes
1. The env variable for .Net Core Console App is called <code>ENVIRONMENT</code>. It's called <code>ASPNETCORE_ENVIRONMENT</code> in ASP.Net Core.
2. Redis C#: https://stackexchange.github.io/StackExchange.Redis/Basics.html 

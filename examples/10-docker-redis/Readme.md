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

   
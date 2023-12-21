
- Configuring BaGet by creating a <code>baget.env</code>

    ```bash
    # The following config is the API Key used to publish packages.
    # You should change this to a secret value to secure your server.
    ApiKey=NUGET-SERVER-API-KEY

    Storage__Type=FileSystem
    Storage__Path=/var/baget/packages
    Database__Type=Sqlite
    Database__ConnectionString=Data Source=/var/baget/baget.db
    Search__Type=Database
    ```
- Running docker
    ```bash
    docker run --rm --name nuget-server -p 5555:80 --env-file baget.env -v "$(pwd)/baget-data:/var/baget" loicsharma/baget:latest
    ```
- Publishing packages
    Publising first package
    ```bash
    dotnet nuget push -s http://localhost:5555/v3/index.json -k NUGET-SERVER-API-KEY package.1.0.0.nupkg
    ```

    Publish first symbol package
    ```bash
    dotnet nuget push -s http://localhost:5555/v3/index.json -k NUGET-SERVER-API-KEY symbol.package.1.0.0.snupkg
    ```

- Browse package by opening the URL http://localhost:5555/ in a browser.
- Restore packages by using the following package source
    ```bash
    http://localhost:5555/v3/index.json
    ```

- To load symbols by using the following symbol location
    ```bash
    http://localhost:5555/api/download/symbols
    ```

# Reference
- https://loic-sharma.github.io/BaGet/installation/docker/
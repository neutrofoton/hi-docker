# Docker Basic
1. Create sql server container

    ``` bash
    docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=P@ssw0rd.1" -p 1433:1433 --name sql2022 -d mcr.microsoft.com/mssql/server:2022-latest
    ```

    > It has an issue when using volume mapping.
    > https://stackoverflow.com/questions/65601077/unable-to-run-sql-server-2019-docker-with-volumes-and-get-error-setup-failed-co
    > User the following docker compose instead.
    
    ``` bash
    sudo docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=P@ssw0rd.1" -p 1433:1433 --name sql2022 -v /var/opt/mssql2022/data:/var/opt/mssql/data -v /var/opt/mssql2022/log:/var/opt/mssql/log -v /var/opt/mssql2022/secrets:/var/opt/mssql/secrets -d mcr.microsoft.com/mssql/server:2022-latest
    ```

2. Check the running container

    ``` bash
    docker ps -a
    ```

3. Connect to the database though SSMS.

   <img src="../images/ss_sql-server-connect-management-studio.png" alt="drawing"/>


# Docker Compose
```bash
services:
  mssql:
    image: mcr.microsoft.com/mssql/server:2022-latest
    user: root
    ports:
      - 1433:1433
    volumes:
      - ./data:/var/opt/mssql/data
      - ./log:/var/opt/mssql/log
      - ./secrets:/var/opt/mssql/secrets
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=P@ssw0rd.1
```

# Reference
- https://stackoverflow.com/questions/65601077/unable-to-run-sql-server-2019-docker-with-volumes-and-get-error-setup-failed-co
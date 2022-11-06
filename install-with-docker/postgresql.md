# Installing PostgreSQL
1. Pull postgress image
   ```
   docker image pull postgres
   ```

2. Create container of postgres
    ```
    # example 1: linux
    docker container run --name postgresql -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres -p 5432:5432 -v /data:/var/lib/postgresql/data -d postgres

    # example 2: mapping the data directory of the PostgreSQL from inside the container to a directory on local machine
    docker container run \
        --name postgresql \
        -e POSTGRES_PASSWORD=postgres \
        -d \
        -v ${PWD}/postgres-docker:/var/lib/postgresql/data \
        -p 5432:5432 postgres 


    # example 3: windows
    docker container run --name postgresql -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres -p 5432:5432 -v /d/Users/myaccount/postgresql/data -d postgres
    ```

    - **postgresql** is the name of the Docker Container.
    - **-e POSTGRES_USER** is the parameter that sets a unique username to the Postgres database.
    - **-e POSTGRES_PASSWORD** is the parameter that allows you to set the password of the Postgres database.
    - **-p 5432:5432** is the parameter that establishes a connection between the Host Port and Docker Container Port. In this case, both ports are given as 5432, which indicates requests sent to the Host Ports will automatically redirect to the Docker Container Port. In addition, 5432 is also the same port where PostgreSQL will be accepting requests from the client.
    - **-v** is the parameter that synchronizes the Postgres data with the local folder. This ensures that Postgres data will be safely present within the Home Directory even if the Docker Container is terminated.
    - **-d** is the parameter that runs the Docker Container in the detached mode, i.e., in the background. If you accidentally close or terminate the Command Prompt, the Docker Container will still run in the background.
    - **postgres** is the name of the Docker image that was previously downloaded to run the Docker Container.

3. Inspect informasi postgres container
    ```
    docker inspect postgresql -f "{{json .NetworkSettings.Networks }}"
    ```


# Installing PgAdmin 4
1. Pull pgadmin4 image
    ```
    docker image pull dpage/pgadmin4
    ```

2. Create container of pgadmin4
    ```
    # oneline command
    docker container run -p 5050:80 --name pgadmin4 -e "PGADMIN_DEFAULT_EMAIL=me@localhost.com" -e "PGADMIN_DEFAULT_PASSWORD=postgres" -d dpage/pgadmin4

    # multiline command
    docker container run -p 5050:80 \
    --name pgadmin4 \
    -e "PGADMIN_DEFAULT_EMAIL=me@localhost.com" \
    -e "PGADMIN_DEFAULT_PASSWORD=postgres" \
    -d dpage/pgadmin4
    ```
3. Open ```http://localhost:5050```

# References
1. https://docs.docker.com/desktop/troubleshoot/topics/
2. https://hevodata.com/learn/docker-postgresql/
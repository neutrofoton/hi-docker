# Installing MySQL 
1. Pulling image and creating mysql container
    ```
    docker container run -d --name mysql -p 3306:3306 -e MYSQL_RANDOM_ROOT_PASSWORD=true mysql
    ```

    To create mysql container with specified password:
       ```
       docker container run -d \
      --name mysql \
      -p 3306:3306 \
      -e MYSQL_ROOT_PASSWORD=your_secure_password \
      mysql

       # example
       docker container run -d \
      --name mysql \
      -p 3306:3306 \
      -e MYSQL_ROOT_PASSWORD=root \                
      mysql

       ```

3. Check the generated random root password from container log
    ```
    docker container logs mysql
    ```

    We will see in the log
   ```
    ....
    ....

    2022-10-23T07:59:29.341590Z 0 [System] [MY-010931] [Server] /usr/sbin/mysqld: ready for connections. Version: '8.0.31'  socket: '/var/run/mysqld/mysqld.sock'  port: 0  MySQL Community Server - GPL.

    2022-10-23 07:59:29+00:00 [Note] [Entrypoint]: Temporary server started. '/var/lib/mysql/mysql.sock' -> '/var/run/mysqld/mysqld.sock'
    
    Warning: Unable to load '/usr/share/zoneinfo/iso3166.tab' as time zone. Skipping it.
    Warning: Unable to load '/usr/share/zoneinfo/leapseconds' as time zone. Skipping it.
    Warning: Unable to load '/usr/share/zoneinfo/tzdata.zi' as time zone. Skipping it.
    Warning: Unable to load '/usr/share/zoneinfo/zone.tab' as time zone. Skipping it.
    Warning: Unable to load '/usr/share/zoneinfo/zone1970.tab' as time zone. Skipping it.
    
    2022-10-23 07:59:30+00:00 [Note] [Entrypoint]: GENERATED ROOT PASSWORD: {RANDOM_PASSWORD_WILL_BE_HERE}
    
    2022-10-23 07:59:30+00:00 [Note] [Entrypoint]: Stopping temporary server
    2022-10-23T07:59:31.003604Z 10 [System] [MY-013172] [Server] Received SHUTDOWN from user root. Shutting down mysqld (Version: 8.0.31).
    2022-10-23T07:59:32.088209Z 0 [System] [MY-010910] [Server] /usr/sbin/mysqld: Shutdown complete (mysqld 8.0.31)  MySQL Community Server - GPL.
    2022-10-23 07:59:33+00:00 [Note] [Entrypoint]: Temporary server stopped
    2022-10-23 07:59:33+00:00 [Note] [Entrypoint]: MySQL init process done. Ready for start up.

    ....
    ....

    ```

    
4. Test connection via any mysql tool.
   You can also test via terminal
   ```bash
   docker exec -it mysql mysql -uroot -pyour_secure_password

   # example
   docker exec -it mysql mysql -uroot -proot
   ```

# Reference
1. https://hub.docker.com/_/mysql

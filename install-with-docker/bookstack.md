# Installation

### Using docker compose
```yaml
---
version: "2"
services:
  bookstack:
    image: lscr.io/linuxserver/bookstack
    container_name: bookstack
    environment:
      - PUID=1000
      - PGID=1000
      - APP_URL=http://127.0.0.1:6875
      - DB_HOST=bookstack_db
      - DB_PORT=3306
      - DB_USER=bookstack
      - DB_PASS=bookstack
      - DB_DATABASE=bookstackapp
    volumes:
      - ./bookstack_app_data:/config
    ports:
      - 6875:80
    restart: unless-stopped
    depends_on:
      - bookstack_db
  bookstack_db:
    image: lscr.io/linuxserver/mariadb
    container_name: bookstack_db
    environment:
      - PUID=1000
      - PGID=1000
      - MYSQL_ROOT_PASSWORD=root
      - TZ=Asia/Jakarta
      - MYSQL_DATABASE=bookstackapp
      - MYSQL_USER=bookstack
      - MYSQL_PASSWORD=bookstack
    volumes:
      - ./bookstack_db_data:/config
    restart: unless-stopped
```

```bash
docker compose -f bookstackapp.yaml up
```

The default username is <code>admin@admin.com</code> with the password of <code>password</code>, access the container at <code>http://dockerhost:6875</code>.

# References
- https://hub.docker.com/r/linuxserver/bookstack
- https://www.youtube.com/watch?v=Mwlu2oCccMU&ab_channel=Steve%27sTechStuff
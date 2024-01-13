```bash
# List all containers (only IDs)
docker ps -aq

# Stop all running containers
docker container stop $(docker ps -aq)

# Remove all containers
docker container rm $(docker ps -aq)

# Remove all images
docker rmi $(docker images -q)

# Remove all none images
docker system prune
```

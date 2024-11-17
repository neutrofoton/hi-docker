
# Miscellaneous


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


Check docker disk consumtion
``` bash
docker system df
```

Clean up the builder chache
``` bash
docker builder prune
```

### The prune command 
```bash
# Removing unused Docker objects (containers, images, networks, volumes) all at once. 
docker system prune

# -a option to delete only unused images from existing containers.
docker image prune -a

# Removing all stopped containers
docker container prune

# Removing all unused volumes
docker volume prune

# Removing all unused network
docker network prune
```



Remove a container upon exit. It will automatically delete the container when it exits. 
It cannot be used together with the -d option (detach mode)
```bash
 docker run —rm image_name
```

Stop and remove all containers
```bash
# displaying the IDs of all the images with the -q option
docker stop $ (docker ps -a -q)
docker rm $ (docker ps -a -q)
```


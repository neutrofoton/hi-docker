
# About Docker
```
# show docker version
docker version

# show detail docker engine (configuration, images, containers etc)
docker info


# old docker command format is: docker <COMMAND> (options)
# new docker command format is: docker <COMMAND> <SUB_COMMAND> (options)
# example: 
# OLD: docker run
# NEW: docker container run 

# show list command 
docker
```

# Docker Image
> - An ***image*** is the application (binaries, libraries and source code) we want to run.
> - Docker's default image "registry" is called Docker Hub (hub.docker.com)
> - New Command Format is <br/>
    ``` docker image <SUB_COMMAND> (options)```


```
# Build docker image
docker build -t hello-docker .

# -t  => tag
# hello-docker => image name
# . => current dic of docker file.
```

```
# list image
docker image ls

# docker pull
docker pull IMAGE_NAME

#ex: pull apache web server
docker image pull httpd

# remove docker image
docker image rm XXXXX

# Showing docker image history
docker image history IMAGE_NAME
docker image history IMAGE_NAME:TAG
```

### Pull Docker Image
```
docker image pull IMAGE_NAME:TAG

# ex:
docker image pull codewithmosh/hello-docker # deafult will pull latest tag
docker image pull ubuntu

```

# Docker Container
> - A ***container*** is an instance of the docker image running as a process
> - We can have many containers running of the same image
> - Show list of sub command of ***container***<br/>
    ``` docker container
    ```
> - New Command Format is <br/>
    ``` docker container <SUB_COMMAND> (options)```


### Docker Container - Run, Start, Stop, Remove
```
# run docker image
docker container run IMAGE_NAME_or_ID

# ex:
docker container run hello-docker2
docker container run 36382b85ef7b

# docker run also will download image (from docker hub if the image locally not exist), create and run a new container.
# ex:
docker container run ubuntu

# run docker image interactivelly
docker container run -it IMAGE_NAME # -it means interactively

# ex:
docker container run -it ubuntu

# we may get error if using Git Bash: the input device is not a TTY.  
# If you are using mintty, try prefixing the command with 'winpty'. Then use the following command:

winpty docker container run -it ubuntu

# Run(create and start) docker as daemon with name
docker container run -d --name node1 nginx:stable-alpine
docker container run -d --name node2 nginx:stable-alpine

# Run (create and start) docker as daemon with name and specific network
docker container run -d --name node1 --network net-custom nginx:stable-alpine
docker container run -d --name node2 --network net-custom nginx:stable-alpine

# Starting docker container
docker container start node1 node2

# Stopping docker daemon
docker container stop node1 node2

# Removing/Deleting container
docker container rm node1
```


### Docker Container - Port Mapping, Changing Port

<img src="https://github.com/neutrofoton/HiDocker/blob/main/images/ss_port_mapping.PNG" alt="drawing" width="75%"/>

> - Format Command <br/>
    ``` 
    docker container run -p HOST_PORT:CONTAINER_PORT image
    ``` <br/>
    ``` 
    docker container run -publish HOST_PORT:CONTAINER_PORT image
    ```

```
# example: creating and starting httpd web server
docker container run -p 8080:80 httpd
```
> Step:
> 1. Stop existing container
> 2. Create a new image from a container’s changes using ***docker commit*** or ***docker container commit***
> 3. Create new container by specifiying the expected port using ***docker container run***


<img src="https://github.com/neutrofoton/HiDocker/blob/main/images/ss_port_mapping_change.PNG" alt="drawing" width="75%"/>

### Docker Container -Ping Between Containers

```
# Ping from container node1 (172.17.0.2) to node2 (172.17.0.3)
docker exec node1 ping node2
docker exec node1 ping 172.17.0.3
```


### Docker Container - Inspect Container Info (network, etc)
```
# inspect docker container
docker container inspect CONTAINER_NAME

#ex:
# we have docker running
docker container run -d --name node1 nginx:stable-alpine

# inspect docker container
docker container inspect node1
```
<img src="https://github.com/neutrofoton/HiDocker/blob/main/images/ss_container_network_id.PNG" alt="drawing" width="75%"/>



# Docker Network
> - Show list of sub command of ***network***<br/>
    ``` docker network
    ```
> - New Command Format is <br/>
    ``` docker network <SUB_COMMAND> (options)```
```

# Showing network driver installed once we installed and run docker
docker network ls

# Inspect network driver. 
# It shows detail info of the network including the containers that use the network driver.
docker network inspect NAME_NETWORK

# ex:
docker network inspect bridge
```

<img src="https://github.com/neutrofoton/HiDocker/blob/main/images/ss_network_bridge_inspect.PNG" alt="drawing" width="75%"/>


### Docker Network - Custom Network
```
# Creating custom network
docker network create NETWORK_NAME

#ex:
docker network create net-custom

# check the created network by:
docker network ls

# Removing custom network
docker network rm NETWORK_NAME

# Removing unused network
docker network prune
```

### Docker Network - Connecting Existing Container to a Custom Network
```
# Connecting container to specific network
docker network connect NETWORK_NAME CONTAINER_NAME

#ex:
docker network connect net-custom node1

# Disconnecting container to specific network
docker network disconnect NETWORK_NAME CONTAINER_NAME
```

<img src="https://github.com/neutrofoton/HiDocker/blob/main/images/ss_network_custom_connect.PNG" alt="drawing" width="75%"/>



# Docker Process
```
# show proses that running
docker ps
docker ps -a
```

# Docker Compose

```
# running docker compose
docker-compose up

# running docker compose with specific filename
docker-compose -f hello.yml up

# starting docker compose in background
docker-compose up -d

# showing process
docker-compose ps

# stopping docker compose
docker-compose stop

# starting docker compose
# https://docs.docker.com/engine/reference/commandline/compose_start/
docker-compose start
```



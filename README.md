
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
> - Show list of sub command of ***image***<br/>
    ``` docker image
    ```
> - New Command Format is <br/>
    ``` docker image <SUB_COMMAND> (options)```


```
# Build docker image from a docker file
docker image build -t hello-docker .

# -t  => tag
# hello-docker => image name
# . => current dic of docker file.
```

```
# list image
docker image ls

# docker pull
docker image pull IMAGE_NAME

#ex: pull apache web server
docker image pull httpd

# remove docker image
docker image rm XXXXX

# Showing docker image history
docker image history IMAGE_NAME
docker image history IMAGE_NAME:TAG
```

### Docker Image - Pull
```
docker image pull IMAGE_NAME:TAG

# ex:
docker image pull codewithmosh/hello-docker # deafult will pull latest tag
docker image pull ubuntu

```

# Docker Container
> - A ***container*** is an instance of the docker image which runs as a process
> - We can have many containers which are running of the same image
> - Show list of sub command of ***container***<br/>
    ``` docker container
    ```
> - New Command Format is <br/>
    ``` docker container <SUB_COMMAND> (options)```


### Docker Container - Run, Start, Stop, Remove, Rename
```
# run docker image
docker container run IMAGE_NAME_or_ID

# ex:
docker container run hello-docker2
docker container run 36382b85ef7b

# docker run will download image (from docker hub if the image locally not exist), create and run a new container.
# ex:
docker container run ubuntu

# run docker image interactivelly
docker container run -it IMAGE_NAME # -it means interactively

# ex:
docker container run -it ubuntu

# we may get error if using Git Bash: the input device is not a TTY.  
# If you are using mintty, try prefixing the command with 'winpty'. Then use the following command:

winpty docker container run -it ubuntu

# Run(create and start) docker as daemon (-d or --detach) with name
docker container run -d --name node1 nginx:stable-alpine
docker container run -d --name node2 nginx:stable-alpine

# OR
docker container run --detach --name node1 nginx:stable-alpine
docker container run --detach --name node2 nginx:stable-alpine

# Run (create and start) docker as daemon with name and specific network
docker container run -d --name node1 --network net-custom nginx:stable-alpine
docker container run -d --name node2 --network net-custom nginx:stable-alpine

# Starting docker container
docker container start node1 node2

# Stopping docker daemon
docker container stop node1 node2

# Removing/Deleting container
docker container rm node1

# Rename container
docker container rename OLD_NAME NEW_NAME
```

### Docker Container - Port Mapping, Changing Port
<img src="https://github.com/neutrofoton/HiDocker/blob/main/images/ss_port_mapping.PNG" alt="drawing" width="75%"/>

<br/>

```
docker container run -p HOST_PORT:CONTAINER_PORT image
docker container run -publish HOST_PORT:CONTAINER_PORT image

# example: creating and starting httpd web server
docker container run -p 8080:80 httpd
```

We will get a "bind" error if the left number (host port) is being used by anything else, even another container.
We can use any port on the left, like <code>8080:80</code> or <code>8888:80</code>, then use <code>localhost:8888</code> when testing.
<br/>

**Step of Changing Port Mapping:**
1. Stop existing container
2. Create a new image from a container’s changes using ***<code>docker commit</code>*** or ***<code>docker container commit</code>***
3. Create new container by specifiying the expected port using ***<code>docker container run</code>***


<img src="https://github.com/neutrofoton/HiDocker/blob/main/images/ss_port_mapping_change.PNG" alt="drawing" width="75%"/>

<br/>

### Docker Container -Ping Between Containers

```
# Ping from container node1 (172.17.0.2) to node2 (172.17.0.3)
docker container exec node1 ping node2
docker container exec node1 ping 172.17.0.3
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


### Docker Container - Logs
```
docker container logs CONTAINER_NAME_or_ID
```

### Docker Container - Process Running in Container
```
docker container top CONTAINER_NAME_or_ID
```

### Docker Container - Running an Interactive Shell in a Docker Container
```
# running shell
docker exec -it CONTAINER_NAME_OR_ID sh

# running bash
docker exec -it CONTAINER_NAME_OR_ID bash
```

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
# creating custom network
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

### Docker Network - Connecting/Disconnecting Existing Container to a Custom Network
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




# About Docker
```
docker version
```

# Docker Image

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
docker pull httpd

# remove docker image
docker image rm XXXXX

# Showing docker image history
docker image history IMAGE_NAME
docker image history IMAGE_NAME:TAG
```

## Pull Docker Image
```
docker pull IMAGE_NAME:TAG

# ex:
docker pull codewithmosh/hello-docker # deafult will pull latest tag
docker pull ubuntu

```

# Docker Container
```
# run docker image
docker run IMAGE_NAME_or_ID

# ex:
docker run hello-docker2
docker run 36382b85ef7b

# docker run also will run and download from docker hub if the image locally not exist.
# ex:
docker run ubuntu

# run docker image interactivelly
docker run -it IMAGE_NAME # -it means interactively

# ex:
docker run -it ubuntu

# we may get error if using Git Bash: the input device is not a TTY.  
# If you are using mintty, try prefixing the command with 'winpty'. Then use the following command:

winpty docker run -it ubuntu

# Run(create and start) docker as daemon with name
docker run -d --name node1 nginx:stable-alpine
docker run -d --name node2 nginx:stable-alpine

# Run (create and start) docker as daemon with name and specific network
docker run -d --name node1 --network net-custom nginx:stable-alpine
docker run -d --name node2 --network net-custom nginx:stable-alpine

# Starting docker container
docker start node1 node2

# Stopping docker daemon
docker stop node1 node2

# Removing/Deleting container
docker rm node1
```



# Docker Process
```
# show proses that running
docker ps
docker ps -a
```

# Docker Network
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

```
# inspect docker container
docker inspect CONTAINER_NAME

#ex:
# we have docker running
docker run -d --name node1 nginx:stable-alpine

# inspect docker container
docker inspect node1
```
<img src="https://github.com/neutrofoton/HiDocker/blob/main/images/ss_container_network_id.PNG" alt="drawing" width="75%"/>


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


## Ping Between Containers

```
# Ping from container node1 (172.17.0.2) to node2 (172.17.0.3)
docker exec node1 ping node2
docker exec node1 ping 172.17.0.3
```

## Connecting Existing Container to a Custom Network
```
# Connecting container to specific network
docker network connect NETWORK_NAME CONTAINER_NAME

#ex:
docker network connect net-custom node1

# Disconnecting container to specific network
docker network disconnect NETWORK_NAME CONTAINER_NAME
```

<img src="https://github.com/neutrofoton/HiDocker/blob/main/images/ss_network_custom_connect.PNG" alt="drawing" width="75%"/>


# Port Mapping

<img src="https://github.com/neutrofoton/HiDocker/blob/main/images/ss_port_mapping.PNG" alt="drawing" width="75%"/>

```
docker run -p HOST_PORT:CONTAINER_PORT image

# example: creating and starting httpd web server
docker run -p 8080:80 httpd
```

## Changing Port
```
# Step:
# 1. Stop existing container
# 2. Create a new image from a container’s changes using docker commit
# 3. Create new container by specifiying the expected port using docker run
```

<img src="https://github.com/neutrofoton/HiDocker/blob/main/images/ss_port_mapping_change.PNG" alt="drawing" width="75%"/>
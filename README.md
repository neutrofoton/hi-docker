
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

### Docker Hub

```
# login
docker login
winpty docker login

# logout
docker logout
```

Check whether we have logged or not
```
cd ~
cat .docker/config.json
```

If we have logged in, the response will be: <br/>
```
{
        "auths": {
            "https://index.docker.io/v1/":{}
        },
        "credsStore": "desktop"
}
```

if not or logged out
```
{
        "auths":{},
        "credsStore": "desktop"
}
```

> To create private repository, create repository first, then uploaded.

# Docker Image
> - An ***image*** is app binaries and the app's dependencies. It's an ordered collection of root filesystem changes and the corresponding execution parameters for use within a container runtime. Inside the image there is not a complete OS. No Kernel, no kernel modules (e.g: drivers). It is just binaries that our application needed, because the host provide the kernel. It is characteristic that differentiate from the VM.
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


```



### Docker Image - Pull
```
docker image pull IMAGE_NAME:TAG

# ex:
docker image pull codewithmosh/hello-docker # deafult will pull latest tag
docker image pull ubuntu

```

### Docker Image - Tag and Push to Docker Hub

Docker image information format: <br/>

```
{user}/{repo}:{tag}
```

```
docker image tag --help

```
Usage:  <br/>
<code>docker image tag ***SOURCE_IMAGE[:TAG]*** ***TARGET_IMAGE[:TAG]***</code>
<br/>
If we don't specify the tag, the default will be **latest**
<p></p>
Example:

```
# re-tag existing image
docker image tag nginx neutrofoton/nginx
```
<img src="https://github.com/neutrofoton/HiDocker/blob/main/images/ss_image_retag.PNG" alt="drawing" width="75%"/>

<br/>

To push to docker hub
```
# push to image hub
docker image push neutrofoton/nginx
```

### Docker Image - History
> - Images are made up of file system changes and metadata
> - Each layer is uniquely identified and only stored once on a host
> - This saves storage space on host and transfer time in push/pull
> - A container is just a single read/write layer on top of image

```
# Showing docker image history
docker image history IMAGE_NAME
docker image history IMAGE_NAME:TAG
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

# check port used by container
docker container port CONTAINER_NAME_OR_ID
docker container port httpd
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

<br/>

Querying the json element of ***docker container inspect*** can be done with the following command:
```
# inspect specific element in json container
docker container inspect --format '{{ .NetworkSettings.IPAddress }}' CONTAINER_NAME_or_ID
docker container inspect --format '{{ .NetworkSettings.IPAddress }}' mysql

docker container inspect --format '{{.NetworkSettings.Networks.bridge.Gateway}}' mysql
```

### Docker Container - Logs
```
# check log of specific container
docker container logs CONTAINER_NAME_or_ID

# example: random password generated on creating mysql container listed in the log.
```

### Docker Container - Process Running in Container
```
# list running processes in a specific container.
docker container top CONTAINER_NAME_or_ID

# show running process of container from the host
ps aux

# show live performance data for all containers
docker container stats
```
<img src="https://github.com/neutrofoton/HiDocker/blob/main/images/ss_container_stats.PNG" alt="drawing" width="75%"/>


### Docker Container - Running an Interactive Shell in a Docker Container

***Parameter description:***
- ***-t*** is pseudo-tty that simulates a real terminal like what ssh does
- ***-i*** is interactive that keeps session open to receive terminal input


<br/>

***1. Docker Container Shell - Executing command at container creation***
```
# container run help
docker container run --help
```

Usage:<br/>
<code>docker container run [OPTIONS] IMAGE **[COMMAND] [ARG...]**</code>

***<b>COMMAND [ARG...]</b> that will be sent to new the container to run***. <br/>
The image has default command to run. But we can change by passing it at the run image command arg.

<br/>

Example:
```
# running bash
docker container run -it --name proxy1 nginx bash

# running shell
docker container run -it --name proxy2 nginx sh

```
<br/>

***2. Docker Container Shell - Getting a shell inside existing container***

```
# container exec help
docker container exec --help
```

Usage:<br/>
<code>docker container exec [OPTIONS] CONTAINER **COMMAND [ARG...]**</code>

***<b>COMMAND [ARG...]</b> that will be sent to the container to run***

<br/>
Example:

```
# running bash
docker container exec -it CONTAINER_NAME_OR_ID bash

# running shell
docker container exec -it CONTAINER_NAME_OR_ID sh


```

### Docker Container -Ping Between Containers

```
# Ping from container node1 (172.17.0.2) to node2 (172.17.0.3)
docker container exec node1 ping node2
docker container exec node1 ping 172.17.0.3

docker container exec -it node1 ping node2
```

# Docker Network
Docker network notes:
- Each container connected to a private virtual network ***bridge***
- Each virtual network routes through NAT firewall on host IP
- All containers on a virtual network can talk to each other without ***-p***

The command format is:
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

# Docker Volumes and Bind Mounts
***Volume*** <br/> 
  Volume is a directory created within Docker's storage directory on the host machine, and Docker manages that directory's content.   

  Volumes are stored in a part of the host filesystem which is managed by Docker. The locations are:
  1. Linux: <code>/var/lib/docker/volumes/</code>
  2. Mac: <code>/$HOME/docker/volumes/</code>
  3. Windows: <code>C:\ProgramData\docker\volumes</code>
  <br/><br/>

   ```
   docker container run --mount type=volume,source=$HOME/docker/volumes/postgres,target=/var/lib/postgresql/data 
   ```

   ```
   docker container run --v $HOME/docker/volumes/postgres:/var/lib/postgresql/data 
   ```

***Bind Mount*** <br/> 
  A file or directory on the host machine is mounted into a container. The file or directory is referenced by its full or relative path on the host machine.
  <br/><br/>
  Bind mounts may be stored anywhere on the host system. They may even be important system files or directories. Non-Docker processes on the Docker host or a Docker container can modify them at any time.
  <br/><br/>

  
   ```
   docker container run --mount type=bind,source=$HOME/my_data_storage,target=/var/lib/postgresql/data 
   ```

   ```
   docker container run --v $HOME/my_data_storage:/var/lib/postgresql/data 
   ```
  
***tmpfs mounts***<br/>
    tmpfs mounts are stored in the host system’s memory only, and are never written to the host system’s filesystem.

  
<img src="https://github.com/neutrofoton/HiDocker/blob/main/images/ss_types-of-mounts.png" alt="drawing" width="75%"/>

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


# References
1. https://docs.docker.com/storage/
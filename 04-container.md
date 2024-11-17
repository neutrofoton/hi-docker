
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

1. Display Only the Latest Lines
```
docker logs --tail 100 <container ID>
```

2. Stream Logs Until a Specific Point in Time
```
# logs written during the first three seconds
docker logs --follow --until=3s

# the logs for the last 30 minutes
docker logs --follow --until=30m
```

3. Stream Logs From a Specific Point in Time
```
docker logs --since 2019-03-02 <container ID>
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
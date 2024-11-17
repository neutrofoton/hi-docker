
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


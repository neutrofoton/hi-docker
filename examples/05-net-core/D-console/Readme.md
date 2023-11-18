1. Build docker image
```
cd 04-net-core-in-docker
docker image build --rm -t neutrofoton/console-net6:latest .
```

2. Check the created image
```
docker image ls | grep neutrofoton
``` 

3. Create container
```
docker container run -d --rm neutrofoton/console-net6:latest
```

4. Check log
```
docker logs --tail 100 <container ID>
```
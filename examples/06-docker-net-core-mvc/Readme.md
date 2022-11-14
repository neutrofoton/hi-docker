1. Build docker image
```
cd 04-net-core-in-docker
docker build --rm -t neutrofoton/hello-net6:latest .
```

2. Check the created image
```
docker image ls | grep neutrofoton
``` 

3. Create container
```
docker run --rm -p 5000:5000 -p 5001:5001 -e ASPNETCORE_HTTP_PORT=https://+:5001 -e ASPNETCORE_URLS=http://+:5000 neutrofoton/hello-net6:latest
```

4. Open in browser
```
http://localhost:5000
```
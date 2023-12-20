1. Build docker image
```
cd A-MVC
docker build --rm -t neutrofoton/hello-net:latest .
```

2. Check the created image
```
docker image ls | grep neutrofoton
``` 

3. Create container
```
docker run --rm -p 5000:5000 -p 5001:5001 -e ASPNETCORE_HTTP_PORT=https://+:5001 -e ASPNETCORE_URLS=http://+:5000 neutrofoton/hello-net:latest
```

4. Open in browser
```
http://localhost:5000
```

# Reference
- https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/building-net-docker-images
1. Build docker image
```
$ cd 04-net-core-in-docker
$ docker build --rm -t neutrolab/netcore-mvc:latest .
```

2. Create container
```
docker run --rm -p 5000:5000 -p 5001:5001 -e ASPNETCORE_HTTP_PORT=https://+:5001 
```
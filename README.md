# DockerCommand

### Common Command
```
docker version
```

### Docker Image

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

# remove docker image
docker image rm XXXXX
```

### Running Docker
```
# run docker image
docker run IMAGE_NAME_or_ID

## ex:
docker run hello-docker2
docker run 36382b85ef7b

# docker run also will run and download from docker hub if the image locally not exist.
## ex:
docker run ubuntu

# run docker image interactivelly
docker run -it IMAGE_NAME # -it means interactively

## ex:
docker run -it ubuntu

### we may get error if using Git Bash: the input device is not a TTY.  If you are using mintty, try prefixing the command with 'winpty'. Then use the following command:

winpty docker run -it ubuntu
```

### Pull Docker Image
```
docker pull IMAGE_NAME:TAG

## ex:
docker pull codewithmosh/hello-docker # deafult will pull *latest* tag
docker pull ubuntu
```

### Docker process
```
# show proses that running
docker ps
docker ps -a
```
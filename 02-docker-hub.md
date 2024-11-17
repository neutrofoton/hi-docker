# Docker Hub

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
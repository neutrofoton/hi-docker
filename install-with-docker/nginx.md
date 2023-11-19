# Using mapping Volume

When we apply mapping volume, the nginx will not generate default index.html in our host mapping directory. Thus we need to provide it manually index.html or any web page.

```bash
docker container run -d -p 8081:80 -v ~/sites/docker/nginx/html:/usr/share/nginx/html --hostname nginx-static --name nginx-static nginx:latest
```

If we get into the container, we can see the file what we have in the container <code>/usr/share/nginx/html</code> exactly the same as we have in the host <code>~/sites/docker/nginx/html</code>

```bash
docker exec -it nginx-static /bin/bash

ls /usr/share/nginx/html
```

### Example hosting web template

In this example I use opensource web template from [Gentelella](https://github.com/ColorlibHQ/gentelella)


```bash
docker container run -d -p 80:80 -v ~/sites/docker/nginx/html/gentelella:/usr/share/nginx/html --hostname gentelella --name gentelella nginx:latest
```

```bash
sudo docker container run -d -p 80:80 -v ${PWD}:/usr/share/nginx/html --name gentelella --hostname gentelella nginx:latest
````

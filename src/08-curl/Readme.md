```bash
docker build -t my-curl .
```

```bash
docker run -d --name my-curl-container my-curl
```

executing curl inside the container
```bash
docker exec my-curl-container curl google.com -v
```
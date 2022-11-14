
## Steps:
<br/>
1. Publish the web app in a <code><b>publish</b></code> directory

```
dotnet publish MyWebMVC/MyWebMVC.csproj --os linux -c release -o publish
```

2. Build a docker image based on the published web app
```
docker image build --rm -t neutrofoton/hello-net6-nosrc:latest .
```

3. Create a docker container
```
winpty docker run --rm -it -p 5000:5000 -p 5001:5001 -e ASPNETCORE_HTTP_PORT=https://+:5001 -e ASPNETCORE_URLS=http://+:5000 neutrofoton/hello-net6-nosrc:latest
```

<code>winpty</code> is needed only when we use git bash terminal.

4. Open in browser
```
http://localhost:5000/
```
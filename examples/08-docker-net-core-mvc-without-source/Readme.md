```
dotnet publish MyWebMVC/MyWebMVC.csproj --os linux -c release -o publish
docker image build --rm -t neutrofoton/hello-net6-nosrc:latest .
winpty docker run --rm -it -p 6000:6000 -p 6001:6001 -e ASPNETCORE_HTTP_PORT=https://+:6001 -e ASPNETCORE_URLS=http://+:6000 neutrofoton/hello-net6-nosrc:latest

```
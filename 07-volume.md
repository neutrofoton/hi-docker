
# Docker Volumes and Bind Mounts


<img src="https://github.com/neutrofoton/HiDocker/blob/main/images/ss_types-of-mounts.png" alt="drawing" width="75%"/>
<br/>
<br/>

### ***Volume***
  Volume is a directory created within Docker's storage directory on the host machine, and Docker manages that directory's content.   

  Volumes are stored in a part of the host filesystem which is managed by Docker. The locations are:
  1. Linux: <code>/var/lib/docker/volumes/</code>
  2. Mac: <code>/$HOME/docker/volumes/</code>
  3. Windows: <code>C:\ProgramData\docker\volumes</code>
  <br/><br/>

   ```
   docker container run --mount type=volume,source=$HOME/docker/volumes/postgres,target=/var/lib/postgresql/data 
   ```

   ```
   docker container run --v $HOME/docker/volumes/postgres:/var/lib/postgresql/data 
   ```

### ***Bind Mount***
  A file or directory on the host machine is mounted into a container. The file or directory is referenced by its full or relative path on the host machine.
  <br/><br/>
  Bind mounts may be stored anywhere on the host system. They may even be important system files or directories. Non-Docker processes on the Docker host or a Docker container can modify them at any time.
  <br/><br/>

  
   ```
   docker container run --mount type=bind,source=$HOME/my_data_storage,target=/var/lib/postgresql/data 
   ```

   ```
   docker container run --v $HOME/my_data_storage:/var/lib/postgresql/data 
   ```
  
###  ***tmpfs mounts***
tmpfs mounts are stored in the host system’s memory only, and are never written to the host system’s filesystem.

  
1. Build docker image

    ```
     docker build -t hello-docker-httpd:1.0.0 .
    ```
2. Checking the image result 

    ```
    # make sure the created image listed in the existing image list
    docker image ls

    # check history (optional)
    docker image history hello-docker-httpd:1.0.0
    ```

3. Starting the web server

    ```
    docker run -d -p 8080:80 hello-docker-httpd:1.0.0
    ```
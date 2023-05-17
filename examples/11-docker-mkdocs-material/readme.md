
``` bash
# build docker image
docker build -t neutro-mkdocs:v1 --build-arg=USER=$(id -u) .

# creating mkdoc directory
mkdir $PWD/mkdoc-working

# creating new site
docker run -it -v $PWD/mkdoc-working:/build neutro-mkdocs:v1 new /build
```

This command will create the following initial files (inside the <code>$PWD/mkdoc-working</code> directory on local system:

``` 
.
./docs
./docs/index.md
./mkdocs.yml

```

``` bash
# Run the builtin development server
docker run -it -p 8789:8000 -v $PWD/mkdoc-working:/build neutro-mkdocs:v1 serve --dev-addr 0.0.0.0:8000 --config-file /build/mkdocs.yml
```

# References
- https://readthedocs.vinczejanos.info/Blog/2021/10/01/How_to_use_MKdocs/
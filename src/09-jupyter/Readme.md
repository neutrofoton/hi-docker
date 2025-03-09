# Installing Jupyter Notebook
1. Run docker command:

    ```bash
    docker run -p 8888:8888 -it --name jupyter_cpp -v ./notebooks:/home/jovyan/work jupyter/datascience-notebook

    ```

2. Open link of Jupyter show in docker log terminal.
    ```text
    http://127.0.0.1:8888/lab?token=xxxxxxxx
    ```

# Installing C++ Kernel
1. Inside the running Jupyter Notebook container, open a terminal (File → New → Terminal) and install <code>xeus-cling</code>
    ```bash
    conda install -c conda-forge xeus-cling -y

    ```

    This step takes times.

2. Create new Notebook (File -> New -> Notebook). Select C++ kernel.
3. Create code
    ```cpp
    #include <iostream>
    std::cout << "Hello, Jupyter with C++!" << std::endl;
    ```

    It should display output

# Installing OpenCV 
1. From Notebook terminal run
    ```bash
    conda install -c conda-forge opencv
    ```

# Conda command
- Update
    ```bash
    conda update conda
    ```

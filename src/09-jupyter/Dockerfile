# Use Jupyter base image
FROM jupyter/datascience-notebook

# Switch to root user to install packages
USER root

# Install system dependencies and OpenCV
RUN apt-get update && apt-get install -y libopencv-dev && rm -rf /var/lib/apt/lists/*

# Switch back to jovyan (non-root user)
USER jovyan

# Install xeus-cling (C++ kernel) with Conda
RUN conda install -c conda-forge xeus-cling -y

# Set default working directory
WORKDIR /home/jovyan

# Expose Jupyter's port
EXPOSE 8888

# Start Jupyter Notebook
CMD ["start.sh", "jupyter", "notebook", "--allow-root", "--NotebookApp.token=''"]

FROM node:alpine
COPY . /app

# mode 1: direct run app.js inside app directory
# CMD node /app/app.js

# mode 2: using working directory
WORKDIR /app
CMD node app.js

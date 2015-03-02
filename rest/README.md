# Documentation

## Install

1. install node.js or io.js
2. restart or relogin (path var only updates on os startup)
2. to install dependencies run from DIR where package.json is, command:
```bash
$ npm install --production
$ npm install -g nodemon
```


## Start App

run command:
```bash
$ nodemon app
```


## REST Interface

1. Open Browser
2. Query localhost:port (port is logged on startup from app)
3. Look at response for API


## REST Routes
- For Infos: `GET /`
- Get Providers `GET /getGeoCodingProviders`
- Get Coordinates from Properties `GET /getCoords`
- Get Properties from Coordinates `GET /getAddress`


## JSON Format
Look at ./JSON.md
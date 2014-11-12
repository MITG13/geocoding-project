'use strict';

var express = require('express');
var app = express();

app.set('json spaces',4);

app.get('/', function (req, res, next) {
    var info = {
        "requests": [
            {
                "url": "/getGeoCodingProviders",
                "type": "get",
                "params": {

                }
            },
            {
                "url": "/getCoords",
                "type": "post",
                "params": {
                    "provider": "PROVIDER",
                    "": ""
                },
                "response": {

                }
            },
            {
                "url": "/getAddress",
                "type": "post",
                "params": {
                    "provider": "PROVIDER",
                    "geometry": {
                        "type": "Point",
                        "coordinates": ["X", "Y"]
                    }
                },
                "response": {

                }
            }
        ]
    };

    res.json(info);
    next();
});

app.get('/id', function (req, res, next) {
    res.send('Which ID?');
    next();
});
app.get('/id/:id', function (req, res, next) {
    res.send('ID: ' + req.params.id);
    next();
});

app.listen(80);
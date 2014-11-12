'use strict';

var fs = require('fs');
var express = require('express');
var app = express();

app.set('json spaces',4);





app.get('/', function (req, res, next) {
    var responseJSON = {
        "requests": [
            {
                "url": "/",
                "request_types": ["get"],
                "params": {

                }
            },
            {
                "url": "/getGeoCodingProviders",
                "request_types": ["get"],
                "params": {

                }
            },
            {
                "url": "/getCoords",
                "request_types": ["get", "post"],
                "params": {
                    "provider": "PROVIDER",
                    "": ""
                }
            },
            {
                "url": "/getAddress",
                "request_types": ["get", "post"],
                "params": {
                    "provider": "PROVIDER",
                    "geometry": {
                        "type": "Point",
                        "coordinates": ["X", "Y"]
                    }
                }
            }
        ]
    };

    res.json(responseJSON);
    next();
});

app.get('/getGeoCodingProviders', function (req, res, next) {
    var responseJSON = {
        "providers": [
            {
                "name": "Google",
                "options": {}
            },
            {
                "name": "Bing",
                "options": {}
            },
            {
                "name": "HERE",
                "options": {}
            }
        ]
    };

    res.json(responseJSON);
    next();
});
app.use('/getCoords', function (req, res, next) {
    var responseJSON = {

    };
    res.json(responseJSON);
    next();
});
app.use('/getAddress', function (req, res, next) {
    var responseJSON = {

    };
    res.json(responseJSON);
    next();
});

app.listen(80);
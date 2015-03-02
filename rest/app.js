'use strict';

var providers = require('./providers');
var express = require('express');
var app = express();

app.set('json spaces', 4);

app.get('/', function (req, res, next) {
    var responseJSON = {
        "requests": [
            {
                "url": "/",
                "request_types": ["get"]
            },
            {
                "url": "/getGeoCodingProviders",
                "request_types": ["get"]
            },
            {
                "url": "/getCoords",
                "request_types": ["get"],
                "params": {
                    "provider": "PROVIDER",
                    "properties": {
                        "address": "",
                        "country": "",
                        "zip": 1234,
                        "city": "",
                        "street": "",
                        "housenumber": ""
                    }
                }
            },
            {
                "url": "/getAddress",
                "request_types": ["get"],
                "params": {
                    "provider": "PROVIDER",
                    "geometry": {
                        "type": "Point",
                        "coordinates": [12, 34]
                    }
                }
            }
        ]
    };

    res.json(responseJSON);
    next();
});

app.get('/getGeoCodingProviders', function (req, res, next) {
    var providerNames = [];

    for (var provider in providers) {
        if (providers.hasOwnProperty(provider)) {
            providerNames.push(providers[provider].name);
        }
    }

    var responseJSON = {
        "providers": providerNames
    };

    res.json(responseJSON);
    next();
});
app.use('/getCoords', function (req, res, next) {
    var provider = req.query.provider.toLowerCase();
    var properties = req.query.properties;

    var selectedProvider = providers[provider];
    if (typeof(properties) === 'string') {
        properties = JSON.parse(properties);
    }

    var errors = [];

    if (!selectedProvider) {
        errors.push('GeoCoding Provider not found!');
    }
    if (typeof properties !== 'object' || Object.keys(properties).length === 0) {
        errors.push('No correct properties found!');
    }

    function callback (responseJSON) {
        res.json(responseJSON);
        next();
    }

    if (errors.length === 0) {
        selectedProvider.getCoords(properties, callback);
    } else {
        callback({
            errors: errors
        });
    }

});
app.use('/getAddress', function (req, res, next) {
    var provider = req.query.provider.toLowerCase();
    var geometry = req.query.geometry;

    var selectedProvider = providers[provider];
    if (typeof(geometry) === 'string') {
        geometry = JSON.parse(geometry);
    }

    var errors = [];

    if (!selectedProvider) {
        errors.push('GeoCoding Provider not found!');
    }
    if (typeof(geometry) !== 'object' || !Array.isArray(geometry.coordinates)) {
        errors.push('No Coordinates found!');
    }

    function callback (responseJSON) {
        res.json(responseJSON);
        next();
    }

    if (errors.length === 0) {
        selectedProvider.getAddress(geometry, callback);
    } else {
        callback({
            errors: errors
        });
    }
});


var port = 8000;
app.listen(port);
console.log('Run on Port: ' + port);

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
                "request_types": ["get", "post"],
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
                "request_types": ["get", "post"],
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
    var provider = req.param('provider').toLowerCase();
    var properties = JSON.parse(req.param('properties'));
    var selectedProvider = providers[provider];

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
    var provider = req.param('provider').toLowerCase();
    var geometry = JSON.parse(req.param('geometry'));
    var selectedProvider = providers[provider];

    var errors = [];

    if (!selectedProvider) {
        errors.push('GeoCoding Provider not found!');
    }
    if (typeof geometry !== 'object' || typeof geometry.coordinates !== 'array') {
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

app.listen(80);
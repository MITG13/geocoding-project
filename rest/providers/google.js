'use strict';

var request = require('request');

exports = module.exports = {};

var requestURL = 'http://maps.googleapis.com/maps/api/geocode/json?';

exports.getCoords = function(properties, cb) {
    var json = {};
    var options = {
        qs: properties
    };

    request.get(requestURL, options, function (error, response, body) {
        if (!error && response.statusCode === 200) {
            console.log(body);
            var location = JSON.parse(body).results[0].geometry.location;
            json = {
                "properties": {},
                "geometry": {
                    "type": "Point",
                    "coordinates": [location.lat, location.lng]
                },
                "epsg": "EPSG:4326"
            };
            cb(json);
        }
    });
};
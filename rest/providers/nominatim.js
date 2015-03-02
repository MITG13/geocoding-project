'use strict';

require('../polyfill.js')();

var request = require('request');

exports = module.exports = {};

exports.name = 'Nominatim';

var requestURL = ' http://nominatim.openstreetmap.org/search?';

exports.getCoords = function(properties, cb) {
    var options = {
        qs: {
            format: 'json',
            addressdetails: 1,
            q: properties.address,
            street: properties.housenumber + ' ' + properties.street,
            city: properties.city,
            county: properties.country,
            country: properties.country,
            postalcode: properties.zip
        }
    };

    request.get(requestURL, options, function (error, response, body) {
        if (!error && response.statusCode === 200) {
            cb(parseJSON(body));
        }
    });
};

var requestURLreverse = 'http://nominatim.openstreetmap.org/reverse?';

exports.getAddress = function(geometry, cb) {
    var options = {
        qs: {
            format: 'json',
            lat: geometry.coordinates[0],
            lon: geometry.coordinates[1]
        }
    };

    request.get(requestURLreverse, options, function (error, response, body) {
        if (!error && response.statusCode === 200) {
            cb(parseJSON(body));
        }
    });
};


function parseJSON(body) {
    if (typeof(body) === 'string') {
        body = JSON.parse(body);
    }

    if (body.length === 0) {
        return {
            errors: ['No address found!']
        };
    } else {
        var result = body[0];

        var props = {
            country: null,
            postcode: null,
            city: null,
            road: null,
            house_number: null
        };

        for (var component in result.address) {
            if (result.address.hasOwnProperty(component)) {
                switch (component) {
                    case "country":
                        props.country = result.address[component];
                        break;
                    case "postcode":
                        props.postcode = result.address[component];
                        break;
                    case "city":
                        props.city = result.address[component];
                        break;
                    case "town":
                        props.city = result.address[component];
                        break;
                    case "village":
                        props.city = result.address[component];
                        break;
                    case "hamlet":
                        props.city = result.address[component];
                        break;
                    case "road":
                        props.road = result.address[component];
                        break;
                    case "path":
                        props.road = result.address[component];
                        break;
                    case "pedestrian":
                        props.road = result.address[component];
                        break;
                    case "house_number":
                        props.house_number = result.address[component];
                        break;
                }
            }
        }

        return {
            "properties": {
                "address": result.display_name,
                "country": props.country,
                "zip": props.postcode,
                "city": props.city,
                "street": props.road,
                "housenumber": props.house_number
            },
            "geometry": {
                "type": "Point",
                "coordinates": [result.lat, result.lon]
            },
            "epsg": "EPSG:4326"
        };
    }
}
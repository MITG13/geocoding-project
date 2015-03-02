'use strict';

require('../polyfill.js')();

var request = require('request');

exports = module.exports = {};

exports.name = 'Google';

var requestURL = 'http://maps.googleapis.com/maps/api/geocode/json?';

exports.getCoords = function(properties, cb) {
    var options = {
        qs: {
            sensor: false,
            address: properties.address
        }
    };

    request.get(requestURL, options, function (error, response, body) {
        if (!error && response.statusCode === 200) {
            cb(parseJSON(body));
        }
    });
};

exports.getAddress = function(geometry, cb) {
    var options = {
        qs: {
            sensor: false,
            latlng: geometry.coordinates[0] + ',' + geometry.coordinates[1]
        }
    };

    request.get(requestURL, options, function (error, response, body) {
        if (!error && response.statusCode === 200) {
            cb(parseJSON(body));
        }
    });
};


function parseJSON(body) {
    if (typeof(body) === 'string') {
        body = JSON.parse(body);
    }
    console.log(body);
    if (body.results.length === 0) {
        return {
            errors: ['No address found!']
        };
    } else {
        var result = body.results[0];

        var props = {
            country: null,
            postal_code: null,
            locality: null,
            route: null,
            street_number: null
        };

        result.address_components.forEach(function (component) {
            if (Array.isArray(component.types)) {
                component.types.find(function (type) {
                    switch (type) {
                        case "country":
                            props.country = component.long_name;
                            break;
                        case "postal_code":
                            props.postal_code = component.long_name;
                            break;
                        case "locality":
                            props.locality = component.long_name;
                            break;
                        case "route":
                            props.route = component.long_name;
                            break;
                        case "street_number":
                            props.street_number = component.long_name;
                            break;
                    }
                });
            }
        });

        return {
            "properties": {
                "address": result.formatted_address,
                "country": props.country,
                "zip": props.postal_code,
                "city": props.locality,
                "street": props.route,
                "housenumber": props.street_number
            },
            "geometry": {
                "type": "Point",
                "coordinates": [result.geometry.location.lat, result.geometry.location.lng]
            },
            "epsg": "EPSG:4326"
        };
    }
}
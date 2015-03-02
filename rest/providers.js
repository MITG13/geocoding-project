'use strict';

var fs = require('fs');

var PROVIDERS_DIR = './providers/';
var PROVIDERS = {};

(function () {
    fs.readdir(PROVIDERS_DIR, function (err, files) {
        if (err) {
            throw err;
        }
        files.forEach(function (file) {
            PROVIDERS[file.slice(0, -3)] = require(PROVIDERS_DIR + file);
        });
        //console.log(PROVIDERS);
    });
})();

module.exports = PROVIDERS;
'use strict';

module.exports = function () {
    var PROVIDERS_DIR = 'providers/';
    var PROVIDERS = [];

    (function () {
        fs.readdir(PROVIDERS_DIR, function (err, files) {
            if (err) {
                throw err;
            }
            files.forEach(function (file) {
                PROVIDERS.push(require('./' + PROVIDERS_DIR + file));
            });
            console.log(JSON.stringify(PROVIDERS));
        });
    })();
};
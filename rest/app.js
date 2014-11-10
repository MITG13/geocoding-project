'use strict';

var express = require('express');
var app = express();

app.get('/', function (req, res, next) {
    res.send('Hello World! You could request GET /id and GET /id/:id');
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
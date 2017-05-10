var express = require('express');
var router = express.Router();

router.post('/adduser', function(req, res) {

    // Set our internal DB variable
    var db = req.db;

    // Get our form values. These rely on the "name" attributes
    var userName = req.body.username;
    var userEmail = req.body.useremail;
    var userPass = req.body.userpassword;

    var crypto = require('crypto');
    var hashMd5 = crypto.createHash('md5').update(userPass).digest("hex");

    // Set our collection
    var collection = db.get('usercollection');

    // Submit to the DB
    collection.insert({
        "username" : userName.toLowerCase(),
        "email" : userEmail,
        "password" : hashMd5
    }, function (err, doc) {
        if (err) {
            // If it failed, return error
            res.send("There was a problem adding the information to the database.");
        }
        else {
            // And forward to success page
            res.redirect("/userlist");
        }
    });
});

router.get('/', function(req, res) {
    res.render('newuser', { title: 'Add New User' });
});

module.exports = router;
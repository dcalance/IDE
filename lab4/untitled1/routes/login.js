var express = require('express');
var router = express.Router();

router.get('/', function(req, res) {
    res.render('login', { title: 'Login :' });
});

router.post('/', function(req, res) {

    // Set our internal DB variable
    var db = req.db;

    // Get our form values. These rely on the "name" attributes
    var userName = req.body.username;
    var userPass = req.body.userpassword;

    var crypto = require('crypto');
    var hashMd5 = crypto.createHash('md5').update(userPass).digest("hex");

    // Set our collection
    var collection = db.get('usercollection');
    collection.findOne({ username : userName }, function (err, item) {
        if(item != undefined) {
            console.log(item.username);
            if (hashMd5 == item.password)
            {
                res.send("Success");
            }
            else
            {
                res.send("Failed to log in");
            }
        }
        else
        {
            res.send("Username not found.");
        }
    });


});

module.exports = router;
var User = require('mongoose').model('User');
exports.renderList = function(req, res) {
    if(req.session.username && req.session.isAdmin){
        data = [];
        data['username'] = req.session.username;
        User.find({},{}, function(err, users){
            console.log(users);
            data['users'] = users;
            res.render('adminUsersList', data);       
        });
    }else{
        res.redirect('/admin' );

    }
};

exports.renderForm = function(req, res) {
    if(req.session.username && req.session.isAdmin){
        data = [];
        data['username'] = req.session.username;
        if(req.query.username){
            User.findById( req.query.username, function(err, user){
                console.log(user);
                data['user'] = user;
                res.render('adminUsersForm', data);       
            });
        }else{
            var user = [];
            user['username'] = "";
            user['email'] = "";
            user['isAdmin'] = "";
            data['user'] = user;
            res.render('adminUsersForm', data);       
        }
    }else{
        res.redirect('/admin' );

    }
};

exports.saveForm = function(req, res){

}

exports.delete = function(req, res){

}


exports.login = function(req, res){
    var User = require('mongoose').model('User');

    let str_username = req.body.username;
    console.log("request username", str_username);
    User.findOneByUsername( str_username , function (err, user) {
       console.log(user);
       console.log(err);
       if(user){
           console.log("not error");
           console.log(user);
           if(user.authenticate(req.body.password)){
               console.log("correct password");
                res.setHeader('content-type', 'text/plain');
                res.send(user._id);
           }else{
			   console.log("incorrect password");		   			   
               res.send("incorrect password");
           }
       }else{
    		console.log("is error");
    		console.log(err);
            res.send("login error");
       }
    });	
}

exports.getInventory = function(req, res){
    var Inventory = require('mongoose').model('Inventory');
    var user_id = req.query.user_id;
    console.log("user id from params", user_id)
    Inventory.find({ user_id: user_id},{}, function(err, inv){
        console.log("inv: ", inv);
        res.send(inv);
    });
}
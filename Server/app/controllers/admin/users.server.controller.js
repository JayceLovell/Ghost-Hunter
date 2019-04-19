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
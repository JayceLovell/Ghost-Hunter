// Create a new 'render' controller method
exports.renderLogin = function(req, res) {

	res.render('adminLogin' );
};

exports.login = function(req, res){
	var User = require('mongoose').model('User');

	let str_username = req.body.username;
    User.findOneByUsername( str_username , function (err, user) {
       console.log(user);
       if(!err){
           console.log("not error");
           console.log(user);
           if(user.authenticate(req.body.password)){
               console.log("correct password");
			   req.session.username = str_username;
			   req.session.isAdmin = user.isAdmin
               res.redirect('/admin/home');
           }else{
			   console.log("incorrect password");
			   req.session.username = undefined;
			   req.session.isAdmin = false;			   			   
               res.render('admin', {error: 'password'});
           }
       }else{
    		console.log("is error");
    		console.log(err);
			req.session.isAdmin = false;			   			   
			req.session.username = undefined;
            res.render('admin', {error: 'database'});
       }
    });	
}

exports.renderHome = function(req, res) {
	if(req.session.username && req.session.isAdmin){
		console.log("username and is admin set true");
		var data = [];
		data['username'] = req.session.username;
		res.render('adminHome', data);	
	}else{
		console.log("could not log in");
		res.redirect('/admin');
	}
};

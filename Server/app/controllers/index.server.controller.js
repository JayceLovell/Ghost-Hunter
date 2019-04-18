// Create a new 'render' controller method
exports.render = function(req, res) {
	// If the session's 'lastVisit' property is set, print it out in the console 
	if (req.session.lastVisit) {
		console.log(req.session.lastVisit);
	}

	// Set the session's 'lastVisit' property
	req.session.lastVisit = new Date();

	// Use the 'response' object to render the 'index' view with a 'title' property
	res.render('index' );

};

exports.renderRegister = function(req, res) {
	
	res.render('register');

};

exports.register = function(req, res){

	var User = require('mongoose').model('User');

    // Create a new instance of the 'User' Mongoose model
    var user = new User(req.body);
    console.log("body: " + req.body);

    // Use the 'User' instance's 'save' method to save a new user document
    user.save(function (err) {
        if (err) {
            // Call the next middleware with an error message
            console.log(err);
            res.render('register', {error: err});

        } else {
			req.session.username = req.body.username;
            
            // Use the 'response' object to send a JSON response
            res.render('register-confirmation', {username: req.session.username});
            
        }
    });
}
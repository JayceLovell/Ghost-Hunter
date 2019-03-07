// Load the 'index' controller
const index = require('../controllers/index.server.controller');
const admin = require('../controllers/admin/index.server.controller');

// Define the routes module' method
module.exports = function(app) {
	// Mount the 'index' controller's 'render' method
    app.get('/', index.render);
    app.get('/admin', admin.renderLogin);

    app.post('/admin', function(req,res){
        res.redirect('/admin/home');    
    });

    app.get('/admin', admin.renderHome);


    //test points
    app.post('/test/profile', function(req,res){
        
    });

    app.post('/test/inventory', function(req, res){

    });

    app.post('/test/events', function(req, res){

    });

};
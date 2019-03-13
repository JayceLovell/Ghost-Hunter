// Load the 'index' controller
const index = require('../controllers/index.server.controller');
const admin = require('../controllers/admin/index.server.controller');
const users = require('../controllers/admin/users.server.controller');
const events = require('../controllers/admin/events.server.controller');
const ghosts = require('../controllers/admin/ghosts.server.controller');

// Define the routes module' method
module.exports = function(app) {
	// Mount the 'index' controller's 'render' method
    app.get('/', index.render);

    app.get('/register', index.renderRegister);
    app.post('/register', function(req, res){
        index.register(req, res);
    });

    app.get('/admin', admin.renderLogin);

    app.get('/admin/home', admin.renderHome);

    //user admin controller
    app.get('/admin/users', users.renderList);
    app.post('/admin/users',function(req, res){
        users.delete(req, res);
    });
    app.get('/admin/users/edit', users.renderForm);
    app.post('/admin/users/edit', function(req, res){
        users.saveForm(req, res);
    });

    //events admin controller
    app.get('/admin/events', events.renderList);
    app.get('/admin/ghosts', ghosts.renderList);

    app.post('/admin', function(req,res){
        admin.login(req,res);  
    });

    app.get('/admin', admin.renderHome);


    //test points
    app.post('/test/profile', function(req,res){
        
    });

    app.post('/test/inventory', function(req, res){

    });

    app.post('/test/events', function(req, res){

    });

    app.get('/game/events', function(req, res){
        events.getEvents(req,res);
    });



};
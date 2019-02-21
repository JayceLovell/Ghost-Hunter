// Load the module dependencies
const config = require('./config');
const mongoose = require('mongoose');

// Define the Mongoose configuration method
module.exports = function() {
	// Use Mongoose to connect to MongoDB
	const db = mongoose.connect(config.db);

	// Load the 'User' model 
	require('../app/models/user.server.model');
	require('../app/models/ghost.server.model');
	require('../app/models/location.server.model');
	require('../app/models/inventory.server.model');
	require('../app/models/events.server.model');



	// Return the Mongoose connection instance
	return db;
};
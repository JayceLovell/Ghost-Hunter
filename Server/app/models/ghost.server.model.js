// Load the Mongoose module and Schema object
const mongoose = require('mongoose');
const Schema = mongoose.Schema;

// Define a new 'UserSchema'
const GhostSchema = new Schema({
    
	name: {
		type: String,
		// Trim the 'name' field
		trim: true,
		// Set a unique 'name' index
		unique: true,
		// Validate 'name' value existance
		required: true
	},
	description: {
		type: String,
		// Trim the 'description' field
		trim: true,
		// Validate 'description' value existance
		required: true
	},
  rarity: {
		type: Number,

		required: true
	}
});


// Create the 'findOneByUsername' static method
GhostSchema.statics.findOneByName = function(name, callback) {
	// Use the 'findOne' method to retrieve a user document
	this.findOne({
		name: new RegExp(name, 'i')
	}, callback);
};

GhostSchema.statics.random = function(callback) {
	this.count(function(err, count) {
	  if (err) {
		return callback(err);
	  }
	  var rand = Math.floor(Math.random() * count);
	  this.findOne().skip(rand).exec(callback);
	}.bind(this));
  };


// Configure the 'UserSchema' to use getters and virtuals when transforming to JSON
GhostSchema.set('toJSON', {
	getters: true,
	virtuals: true
});

// Create the 'User' model out of the 'UserSchema'
mongoose.model('Ghost', GhostSchema);
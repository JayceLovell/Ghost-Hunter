// Load the Mongoose module and Schema object
const mongoose = require('mongoose');
const Schema = mongoose.Schema;

// Define a new 'UserSchema'
const LocationSchema = new Schema({
    
	name: {
		type: String,
		required: true
    },
    long:{
        type: Number,
        required: true
    },
    lat:{
        type: Number,
        required: true
    }
});


// Create the 'findOneByUsername' static method
LocationSchema.statics.findByName = function(name, callback) {
	// Use the 'findOne' method to retrieve a user document
	this.find({
		name: new RegExp(name, 'i')
	}, callback);
};


// Configure the 'UserSchema' to use getters and virtuals when transforming to JSON
LocationSchema.set('toJSON', {
	getters: true,
	virtuals: true
});

// Create the 'User' model out of the 'UserSchema'
mongoose.model('Location', LocationSchema);
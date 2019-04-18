// Load the Mongoose module and Schema object
const mongoose = require('mongoose');
const Schema = mongoose.Schema;

// Define a new 'UserSchema'
const InventorySchema = new Schema({
    
	user_id: {
		type: mongoose.Schema.ObjectId,
        ref: 'User',
		required: true
    },
    ghost_id:{
        type: mongoose.Schema.ObjectId,
        ref: 'Ghost',
        required: true
    },
    quantity:{
        type: Number,
        default: 1,
        required: true
    }
});


// Create the 'findOneByUsername' static method
InventorySchema.statics.findByUsername = function(username, callback) {
	// Use the 'findOne' method to retrieve a user document
	this.find({
		username: new RegExp(username, 'i')
	}, callback);
};


// Configure the 'UserSchema' to use getters and virtuals when transforming to JSON
InventorySchema.set('toJSON', {
	getters: true,
	virtuals: true
});

// Create the 'User' model out of the 'UserSchema'
mongoose.model('Inventory', InventorySchema);
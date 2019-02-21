// Load the Mongoose module and Schema object
const mongoose = require('mongoose');
const Schema = mongoose.Schema;

// Define a new 'UserSchema'
const EventSchema = new Schema({
    
	id: {
		type: Number,
		required: true
    },
    locationName:{
        type: String,
        required: true
    },
    ghostName:{
        type: String,
        required: true
    }
});



// Configure the 'UserSchema' to use getters and virtuals when transforming to JSON
EventSchema.set('toJSON', {
	getters: true,
	virtuals: true
});

// Create the 'User' model out of the 'UserSchema'
mongoose.model('Event', EventSchema);
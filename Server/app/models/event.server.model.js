// Load the Mongoose module and Schema object
const mongoose = require('mongoose');
const Schema = mongoose.Schema;

// Define a new 'UserSchema'
const EventSchema = new Schema({
    
    location_id:{
        type: mongoose.Schema.ObjectId,
        ref: 'Location',
        required: true
    },
    ghost_id:{
        type: mongoose.Schema.ObjectId,
        ref: 'Ghost',
        required: true
    },
    expireTime:{
        type: Date,
        default: Date.now() + 300000
    }
});



// Configure the 'UserSchema' to use getters and virtuals when transforming to JSON
EventSchema.set('toJSON', {
	getters: true,
	virtuals: true
});

// Create the 'User' model out of the 'UserSchema'
mongoose.model('Event', EventSchema);
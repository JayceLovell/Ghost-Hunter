// Set the 'NODE_ENV' variable
process.env.NODE_ENV = process.env.NODE_ENV || 'development';
var port = process.env.PORT || 3000

// Load the module dependencies
const configureMongoose = require('./config/mongoose');
const configureExpress = require('./config/express');

// Create a new Mongoose connection instance
const db = configureMongoose();

// Create a new Express application instance
const app = configureExpress();

// Use the Express application instance to listen to the '3000' port
// app.listen(port);

const server = require('http').createServer(app);
const io = require('socket.io')(server);
io.on('connection', client => {
    console.log("client connected");
    client.on('event', data => { 
        
     });
    client.on('disconnect', () => { 
        
    });
  });
server.listen(port);

// Log the server status to the console
console.log('Server running at http://localhost:'+ port +'/');
console.log('Socket running at ws://localhost:'+ port +'/');

// Use the module.exports property to expose our Express application instance for external usage
module.exports = app;
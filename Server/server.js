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
server.listen(port);

var Event = require('mongoose').model('Event');
var Location = require('mongoose').model('Location');
var Ghost = require('mongoose').model('Ghost');


var http = require("http");
  setInterval(function() {
    http.get("http://ghost-hunter-game.herokuapp.com");
}, 300000);

var minutes = 0.5, the_interval = minutes * 60 * 1000;
setInterval( function() {
  console.log("Perform event check and update if required")
  RenewEvents();
}, the_interval);

function RenewEvents() {
  console.log("Perform event check and update if required")
  try{
      
    Event.find({},{}, function(err, events){
      var date = new Date(); 
      var timenow = date.getTime();
      events.forEach(event => {
        let timediff = ( event.expireTime.getTime() - timenow ) / 1000;
        if(timediff > 0){
            // console.log('time difference > 0');
        }else{
          var data = event;
          Ghost.random(function(err, ghost){
              console.log("random ghost error", err)
              data.ghost_id = ghost._id
              Location.random( function(err2, location){
                  console.log("random location error", err2)
                  data.location_id = location._id;
                  data.expireTime = undefined;
                  let updatedEvent = new Event(data);
                  updatedEvent.save( function(err3){
                      console.log("saved event", updatedEvent);
                      console.log("saved error", err3);
                  });   
              });
          });
        }
      });
    });
  }
  catch(e){
    console.log("error ", e)
  }
}

// Log the server status to the console
console.log('Server running at http://localhost:'+ port +'/');
RenewEvents();
// Use the module.exports property to expose our Express application instance for external usage
module.exports = app;
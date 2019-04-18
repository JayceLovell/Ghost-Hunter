var Event = require('mongoose').model('Event');
var Location = require('mongoose').model('Location');
var Ghost = require('mongoose').model('Ghost');
exports.renderList = function(req, res) {
    if(req.session.username && req.session.isAdmin){
        data = [];
        data['username'] = req.session.username;
        Event.find( {},{}, function(err,events){
            console.log(events)
            data['events'] = events;

            res.render('adminEventList', data);       
        });   
    }else{
        res.redirect('/admin' );

    }
};

exports.renderForm = function(req, res) {
    
    if(req.session.username && req.session.isAdmin){
        Ghost.find({},{}, function(err, ghosts){
            Location.find({},{}, function(err, locations){
                if(req.query._id){
                    // console.log("edit");
                    Event.find({ _id: req.query._id},{}, function(err,event){
                        data = [];
                        data['username'] = req.session.username;
                        data['location_id']= event[0].location_id;
                        data['ghost_id']= event[0].ghost_id;
                        data['expiry']= event[0].expiry;
                        data['ghosts'] = ghosts;
                        data['locations'] = locations;
                        res.render('adminEventForm',data);
                    });
                }else{
                        data = [];
                        data['username'] = req.session.username;
                        data['location_id']="";
                        data['ghost_id']= "";
                        data['expiry']= "";
                        data['ghosts'] = ghosts;
                        data['locations'] = locations;
                    // console.log("create");
                    console.log(data);
            
                res.render('adminEventForm',data);
                }
            });
        });
        
        
    }else{
        res.redirect('/admin' );

    }
    
};

exports.getEvents = async function(req, res){
    Event.find({},{}, function(err, events){
        var output = [];
        var date = new Date(); 
        var timenow = date.getTime();
        
        
        events.forEach(event => {
            let timediff = ( event.expireTime.getTime() - timenow ) / 1000;
            // console.log("time difference", timediff);
            if(timediff > 0){
                var formatedEvent = {
                    "expireTime": timediff,
                    "_id": event._id,
                    "longitude": event.location_id.long,
                    "latitude": event.location_id.lat,
                    "ghost_id": event.ghost_id._id,
                    "rarity": event.ghost_id.rarity,
                    "ghost_name": event.ghost_id.name
                }

                output.push(formatedEvent);
            }else{
                // var data = event;
                // Ghost.random(function(err, ghost){
                //     console.log("random ghost error", err)
                //     data.ghost_id = ghost._id
                //     Location.random( function(err2, location){
                //         console.log("random location error", err2)
                //         data.location_id = location._id;
                //         data.expireTime = undefined;
                //         let updatedEvent = new Event(data);
                //         updatedEvent.save( function(err3){
                //             console.log("save error", err3);
                //         });   
                //     });
                // });
                
                // Event.remove({_id: event._id}, function(err0){
                //     console.log("event remove err", err0);
                //     Location.random( function(err, location){
                //         console.log("err", err);
                //         console.log("location", location);
                //         Ghost.random( function(err2, ghost){
                //             console.log("err", err2);
                //             console.log("ghost", ghost);
                //             let data = [];
                //             data['ghost_id'] = ghost._id;
                //             data['location_id'] = location._id;
                //             let event = new Event(data);
                //             event.save(function (err3){
                //                 console.log("event save error", err3);
                //             });
                //         });
                //     });
                // });
            }
        });
        res.send(output);
    }).populate('location_id').populate('ghost_id');
}

exports.saveEvent = function(req, res) {
    if(req.session.username && req.session.isAdmin){
        if(req.query._id){
            
            Event.findOneAndUpdate( {_id: req.query._id} , req.body ,{}, function(err, numAffected){
                if(err){
                    data = [];
                    data['username'] = req.session.username;
                    data['location_id'] = req.body.location_id;
                    data['ghost_id'] = req.body.ghost_id;
                    data['expiry'] = req.body.expiry;
                    res.render('adminEventForm', data)
                }else{
                    res.redirect('/admin/events');
                }
            });
        }else{
            var event = new Event(req.body);

             // Use the 'User' instance's 'save' method to save a new user document
             event.save(function (err) {
                if(err){
                    console.log(err);
                    data = [];
                    data['username'] = req.session.username;
                     data['location_id'] = req.body.location_id;
                     data['ghost_id'] = req.body.ghost_id;
                     data['expiry'] = req.body.expiry;
                    res.render('adminEventForm', data)
                }else{
                    res.redirect('/admin/events');
                }
            });
        }
    }else{
        res.redirect('/admin' );
    }
}


exports.catch = function(req,res){

    var user_id = req.body.user_id;
    console.log("user_id", user_id);
    var ghost_id = req.body.ghost_id;
    console.log("ghost_id", ghost_id);

    var Inventory =  require('mongoose').model('Inventory');

    var data = {
        'user_id': user_id,
        'ghost_id': ghost_id
    }
    var inv = new Inventory(data);

    inv.save( function(err3){
        console.log("save error", err3);
    });  

}



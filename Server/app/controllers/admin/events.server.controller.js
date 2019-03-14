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

        if(req.query._id){
            // console.log("edit");
            Event.find({ _id: req.query._id},{}, function(err,event){
                data = [];
                data['username'] = req.session.username;
                data['location_id']= event[0].location_id;
                data['ghost_id']= event[0].ghost_id;
                data['expiry']= event[0].expiry;
                console.log(event);                
                console.log(event[0]);                
                // res.render('adminEventForm',data);
                console.log(data);
    
                res.render('adminEventForm',data);
            });
        }else{
                data = [];
                data['username'] = req.session.username;
                data['location_id']="";
                data['ghost_id']= "";
                data['expiry']= "";
            // console.log("create");
            console.log(data);
    
        res.render('adminEventForm',data);
        }
        
    }else{
        res.redirect('/admin' );

    }
    
};

exports.getEvents = function(req, res){
    Event.find({},{}, function(err, events){
        res.send(events)
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
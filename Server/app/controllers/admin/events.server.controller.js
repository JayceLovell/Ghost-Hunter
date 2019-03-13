var Event = require('mongoose').model('Event');
exports.renderList = function(req, res) {
    if(req.session.username && req.session.isAdmin){
        data = [];
        data['username'] = req.session.username;
        Event.find({},{}, function(events){
            data['events'] = events;
            res.render('adminGhostList', data);       
        });
    }else{
        res.redirect('/admin' );

    }
};

exports.getEvents = function(req, res){
    Event.find({},{},  function(events){
        data = events;
        res.send(data);       
    });
}
var Event = require('mongoose').model('Event');
exports.renderList = function(req, res) {
    if(req.session.username && req.session.isAdmin){
        data = [];
        data['username'] = req.session.username;
        Event.findAll( function(events){
            data['events'] = events;
            res.render('adminEventList', data);       
        });
    }else{
        res.redirect('/admin' );

    }
};
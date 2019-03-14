var Location = require('mongoose').model('Location');
exports.renderList = function(req, res) {
    if(req.session.username && req.session.isAdmin){
        data = [];
        data['username'] = req.session.username;
        Location.find( {},{}, function(err,locations){
            console.log(locations)
            data['locations'] = locations;
            res.render('adminLocationList', data);       
        });
    }else{
        res.redirect('/admin' );

    }
};

exports.renderForm = function(req, res) {
    
    if(req.session.username && req.session.isAdmin){

        if(req.query._id){
            // console.log("edit");
            Location.find({ _id: req.query._id},{}, function(err,location){
                data = [];
                data['username'] = req.session.username;
                data['name']= location[0].name;
                data['long']=location[0].long;
                data['lat']=location[0].lat;
                console.log(location);                
                console.log(location[0]);                
                // res.render('adminLocationForm',data);
                console.log(data);
    
                res.render('adminLocationForm',data);
            });
        }else{
                data = [];
                data['username'] = req.session.username;
                data['name']     = "";
                data['long']      = "";
                data['lat']      = "";
            // console.log("create");
            console.log(data);
    
        res.render('adminLocationForm',data);
        }
        
    }else{
        res.redirect('/admin' );

    }
    
};

exports.saveLocation = function(req, res) {
    if(req.session.username && req.session.isAdmin){
        if(req.query._id){
            
            Location.findOneAndUpdate( {_id: req.query._id} , req.body ,{}, function(err, numAffected){
                if(err){
                    data = [];
                    data['username'] = req.session.username;
                    data['name'] = req.body.name;
                    data['description'] = req.body.description;
                    data['rarity'] = req.body.rarity;
                    res.render('adminLocationForm', data)
                }else{
                    res.redirect('/admin/locations');
                }
            });
        }else{
            var location = new Location(req.body);

             // Use the 'User' instance's 'save' method to save a new user document
             location.save(function (err) {
                if(err){
                    data = [];
                    data['username'] = req.session.username;
                    data['name'] = req.body.name;
                    data['description'] = req.body.description;
                    data['rarity'] = req.body.rarity;
                    res.render('adminLocationForm', data)
                }else{
                    res.redirect('/admin/locations');
                }
            });
        }
    }else{
        res.redirect('/admin' );
    }
}
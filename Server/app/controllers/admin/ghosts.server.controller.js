var Ghost = require('mongoose').model('Ghost');
exports.renderList = function(req, res) {
    if(req.session.username && req.session.isAdmin){
        data = [];
        data['username'] = req.session.username;
        Ghost.find( {},{}, function(err,ghosts){
            console.log(ghosts)
            data['ghosts'] = ghosts;
            res.render('adminGhostList', data);       
        });
    }else{
        res.redirect('/admin' );

    }
};

exports.renderForm = function(req, res) {
    
    if(req.session.username && req.session.isAdmin){

        if(req.query._id){
            // console.log("edit");
            Ghost.find({ _id: req.query._id},{}, function(err,ghost){
                data = [];
                data['username'] = req.session.username;
                data['name']= ghost[0].name;
                data['description']=ghost[0].description;
                data['rarity']=ghost[0].rarity;
                console.log(ghost);                
                console.log(ghost[0]);                
                // res.render('adminGhostForm',data);
                console.log(data);
    
                res.render('adminGhostForm',data);
            });
        }else{
                data = [];
                data['username'] = req.session.username;
                data['name']       ="";
                data['description'] ="";
                data['rarity']      ="";
            // console.log("create");
            console.log(data);
    
        res.render('adminGhostForm',data);
        }
        
    }else{
        res.redirect('/admin' );

    }
    
};

exports.saveGhost = function(req, res) {
    if(req.session.username && req.session.isAdmin){
        if(req.query._id){
            
            Ghost.findOneAndUpdate( {_id: req.query._id} , req.body ,{}, function(err, numAffected){
                if(err){
                    data = [];
                    data['username'] = req.session.username;
                    data['name'] = req.body.name;
                    data['description'] = req.body.description;
                    data['rarity'] = req.body.rarity;
                    res.render('adminGhostForm', data)
                }else{
                    res.redirect('/admin/ghosts');
                }
            });
        }else{
            var ghost = new Ghost(req.body);

             // Use the 'User' instance's 'save' method to save a new user document
             ghost.save(function (err) {
                if(err){
                    data = [];
                    data['username'] = req.session.username;
                    data['name'] = req.body.name;
                    data['description'] = req.body.description;
                    data['rarity'] = req.body.rarity;
                    res.render('adminGhostForm', data)
                }else{
                    res.redirect('/admin/ghosts');
                }
            });
        }
    }else{
        res.redirect('/admin' );
    }
}
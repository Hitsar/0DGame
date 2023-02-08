mergeInto(LibraryManager.library, {

	SaveExtern: function(date) {
    	var dateString = UTF8ToString(date);
    	var myobj = JSON.parse(dateString);
    	player.setData(myobj);
  	},

  	LoadExtern: function(){
    	player.getData().then(_date => {
        	const myJSON = JSON.stringify(_date);
        	myGameInstance.SendMessage('Progress', 'SetPlayerInfo', myJSON);
    	});
 	},

 	SetToLeaderboard : function(value){
    	ysdk.getLeaderboards()
      	.then(lb => {
        lb.setLeaderboardScore('Top', value);
      });
  	},

 
    ShowAdv : function(){
        ysdk.adv.showFullscreenAdv({
          callbacks: {
          onClose: function(wasShown) {
            console.log("------------- closed --------------");
            // some action after close
        	},
          onError: function(error) {
            // some action on error
        	}
        }
        })
    },
  });
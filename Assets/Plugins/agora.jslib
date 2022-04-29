mergeInto(LibraryManager.library, {
  Init: function(channel, userId) {
    var parsedChannel = UTF8ToString(channel);
    var parsedUserId = UTF8ToString(userId);
    init(parsedChannel, parsedUserId);
  },

  ToggleMic: function(){
    toggleMic();
  },

  ToggleVideo: function(){
    toggleVideo();
  },

  Subscribe: function(uid){
    var parsedUid = UTF8ToString(uid);
    subscribeWhenNear(parsedUid);
  },

  Unsubscribe: function(uid){
    var parsedUid = UTF8ToString(uid);
    unsubscribeWhenFar(parsedUid);
  },

});
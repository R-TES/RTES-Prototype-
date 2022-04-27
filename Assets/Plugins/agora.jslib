mergeInto(LibraryManager.library, {
  Init: function(channel, userId) {
    var parsedChannel = UTF8ToString(channel);
    var parsedUserId = UTF8ToString(userId);
    init(parsedChannel, parsedUserId);
  },
});
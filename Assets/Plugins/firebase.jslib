mergeInto(LibraryManager.library, {

  Hello: function () {
    window.alert("Hello, world!");
  },

  HelloString: function (gameObject, callback) {
    var parsedGameObject = Pointer_stringify(gameObject);
    var parsedCallback = Pointer_stringify(callback);
    window.unityInstance.SendMessage(parsedGameObject, parsedCallback, "5");
  },

    GetDocument: function (collectionPath, documentId, objectName, callback, fallback) {
        var parsedPath = Pointer_stringify(collectionPath);
        var parsedId = Pointer_stringify(documentId);
        var parsedObjectName = Pointer_stringify(objectName);
        var parsedCallback = Pointer_stringify(callback);
        var parsedFallback = Pointer_stringify(fallback);
        
        var docRef = firebase.firestore().collection(parsedPath).doc(parsedId);
        docRef.get().then(function(doc){
            if (doc.exists) {
                window.unityInstance.SendMessage(parsedObjectName, parsedCallback, JSON.stringify(doc.data()));
            } else {
                console.log("No such document!");
            }
        }).catch(function(error){
            console.log("Error getting document:", error);
        });
    }

});
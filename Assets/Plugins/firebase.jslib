mergeInto(LibraryManager.library, {
    clean: function(obj) {
        for (var propName in obj) {
            if (obj[propName] == "" || obj[propName] === null || obj[propName] === undefined) {
                delete obj[propName];
            }
        }
        return obj;
    },

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
    },

    AddDocument: function (collectionPath, data){
        var parsedPath = Pointer_stringify(collectionPath);
        var parsedData = Pointer_stringify(data);
        firebase.firestore().collection(parsedPath).add(JSON.parse(parsedData)).then(function(docRef){
            console.log("successfully added document: ", docRef.id);
        }).catch(function(error){
            console.error("Error adding document: ", error);
        });
    },

    SetDocument: function (collectionPath, documentId, data){
        var parsedPath = Pointer_stringify(collectionPath);
        var parsedId = Pointer_stringify(documentId);
        var parsedData = Pointer_stringify(data);
        var cleanedData = clean(JSON.parse(parsedData));

        firebase.firestore().collection(parsedPath).doc(parsedId).set(cleanedData).then(function(){
            console.log("update successful");
        }).catch(function(error) {
            console.log(error);
        });
    }

});
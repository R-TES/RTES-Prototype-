using System.Runtime.InteropServices;

namespace Scripts
{
    public static class FirebaseFirestore
    {

        [DllImport("__Internal")]
        public static extern void GetDocument(string collectionPath, string documentId, string objectName,
            string callback, string fallback);

        [DllImport("__Internal")]
        public static extern void GetDocumentsInCollection(string collectionPath, string objectName, string callback,
            string fallback);

    
        [DllImport("__Internal")]
        public static extern void SetDocument(string collectionPath, string documentId, string value, string objectName,
            string callback,
            string fallback);

        [DllImport("__Internal")]
        public static extern void AddDocument(string collectionPath, string value, string objectName, string callback,
            string fallback);

    
        [DllImport("__Internal")]
        public static extern void UpdateDocument(string collectionPath, string documentId, string value,
            string objectName, string callback,
            string fallback);

    
        [DllImport("__Internal")]
        public static extern void DeleteDocument(string collectionPath, string documentId, string objectName,
            string callback, string fallback);


        [DllImport("__Internal")]
        public static extern void DeleteField(string collectionPath, string documentId, string field, string objectName,
            string callback, string fallback);

        
        [DllImport("__Internal")]
        public static extern void AddElementInArrayField(string collectionPath, string documentId, string field,
            string value, string objectName, string callback, string fallback);

    
        [DllImport("__Internal")]
        public static extern void RemoveElementInArrayField(string collectionPath, string documentId, string field,
            string value, string objectName, string callback, string fallback);

    
        [DllImport("__Internal")]
        public static extern void IncrementFieldValue(string collectionPath, string documentId, string field,
            int increment, string objectName, string callback, string fallback);

    
        [DllImport("__Internal")]
        public static extern void ListenForDocumentChange(string collectionPath, string documentId,
            bool includeMetadataChanges, string objectName, string onDocumentChange,
            string fallback);

    
        [DllImport("__Internal")]
        public static extern void StopListeningForDocumentChange(string collectionPath, string documentId,
            string objectName, string callback, string fallback);

    
        [DllImport("__Internal")]
        public static extern void ListenForCollectionChange(string collectionPath, bool includeMetadataChanges,
            string objectName, string onCollectionChange, string fallback);

       
        [DllImport("__Internal")]
        public static extern void StopListeningForCollectionChange(string collectionPath, string objectName,
            string callback, string fallback);
    }
}
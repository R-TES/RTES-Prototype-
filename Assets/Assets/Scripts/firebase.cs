// using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


[System.Serializable]
class Room{
    public string name;
    public string template;
    public string owner;
}

public class firebase : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Hello();

    [DllImport("__Internal")]
    private static extern void HelloString(string str, string callback);

    [DllImport("__Internal")]
    public static extern void GetDocument(string collectionPath, string documentId, string objectName,
            string callback, string fallback);

    void Start() {
        Hello();
        HelloString(gameObject.name, "callback");
        GetDocument("Rooms", "tSSPMUrsoiU6lYPGDkme", gameObject.name, "callback", "fallback");
    }
    private void callback(string data){
        Room room = JsonUtility.FromJson<Room>(data);
        Debug.Log(room.name);
        Debug.Log(room.template);
        Debug.Log(room.owner);

    }

    private void fallback(string error){
        Debug.Log(error);
    }
}



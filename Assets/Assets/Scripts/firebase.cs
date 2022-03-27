using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[System.Serializable]
class Room{
    public string name;
    public string owner;
    [SerializeField]
    public List<string> roomObjects;
    public string template;
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

    [DllImport("__Internal")]
    public static extern void AddDocument(string collectionPath, string data);

    [DllImport("__Internal")]
    public static extern void SetDocument(string collectionPath, string documentId, string data);

    void Start() {
        Hello();
        HelloString(gameObject.name, "callback");
        GetDocument("Rooms", "tSSPMUrsoiU6lYPGDkme", gameObject.name, "callback", "fallback");
        Room newRoom = new Room();
        newRoom.name = "Office2";
        newRoom.owner = "Users/zd26igJS15tCTNchXaaQ";
        newRoom.roomObjects = new List<string> {"RoomObjects/VZLw9B7HMFZA7mlNIqVR"};
        // newRoom.template = "Templates/GfGIZ9UkGk9T19Strtll";
        string json = JsonUtility.ToJson(newRoom);
        print(json);
        // AddDocument("Rooms", json);
        SetDocument("Rooms", "tSSPMUrsoiU6lYPGDkme", json);
    }
    private void callback(string data){
        Debug.Log(data);
        Room room = JsonUtility.FromJson<Room>(data);
        Debug.Log(room.name);
        Debug.Log(room.template);
        Debug.Log(room.owner);
        foreach(string roomObj in room.roomObjects){
            Debug.Log(roomObj);
        }

    }

    private void fallback(string error){
        Debug.Log(error);
    }
}



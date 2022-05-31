using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class FirebaseFirestore : MonoBehaviour {
    public string uid;
    public string res;
    public Room room;
    public CreateOrJoinFirestoreRoom obj;

    public void getUserInfo(string uid){
        Console.WriteLine("inside firestore script");
        FirestoreHandler.GetDocument("Users", uid, gameObject.name, "setUserInfo", "displayErrorObject");
    }

    public void setUserInfo(string data){
        obj.user = Serializer<User>.toObject(data);
        obj.isUserSet = true;
    }

    public void getRoomInfo(string roomId){
        FirestoreHandler.GetDocument("Rooms", roomId, gameObject.name, "setRoomInfo", "displayErrorObject");
    }

    public void setRoomInfo(string data){
        obj.room = Serializer<Room>.toObject(data);
        obj.isRoomSet = true;
    }

    public void displayErrorObject(string error){
        UnityEngine.Debug.Log("firestore call failed: " + error);
    }
}

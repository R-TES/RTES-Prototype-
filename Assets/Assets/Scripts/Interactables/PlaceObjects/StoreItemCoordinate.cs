using System.Runtime.Serialization;
using System;
using System.Diagnostics;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class StoreItemCoordinate {
    public void save(string roomId, string prefab, float xVal, float yVal){
        RoomObjectPosition pos = new RoomObjectPosition(prefab, xVal, yVal);
        string deserializedObject = Serializer<RoomObjectPosition>.toJSON(pos);
        FirestoreHandler.AddElementInArrayField("Rooms", roomId, "roomObjects", deserializedObject);
    }
}
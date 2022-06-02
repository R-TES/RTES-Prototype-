using Newtonsoft.Json;
using System.Collections.Generic;


[System.Serializable]
public class Room {
    public string id;
    public string name;
    public string owner;
    public List<RoomObjectPosition> roomObjects;
    public string template;
}

[System.Serializable]
public class RoomObjectPosition {
    public string name;
    public float xVal;
    public float yVal;

    public RoomObjectPosition(string name, float xVal, float yVal){
        this.name = name;
        this.xVal = xVal;
        this.yVal = yVal;
    }
}

[System.Serializable]
public class User {
    public string id;
    public string username;
    public string avatar;
    public string designation;
    public string email;
    public string mobile;
    public List<Room> rooms;
}

[System.Serializable]
public class Template {
    public string name;
    public string scene;
}

[System.Serializable]
public class RoomObject {
    public string name;
}

[System.Serializable]
public class Avatar {
    public string name;
    public string color;
}

public class Serializer<T>{
    public static string toJSON(T obj){
        return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None, 
        new JsonSerializerSettings { 
            NullValueHandling = NullValueHandling.Ignore
        });
    }
    public static T toObject(string json){
        return JsonConvert.DeserializeObject<T>(json);
    }
}


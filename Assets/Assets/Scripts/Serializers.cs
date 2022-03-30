using Newtonsoft.Json;
using System.Collections.Generic;

namespace Scripts{

    [System.Serializable]
    public class Room {
        public string  name;
        public string owner;
        public List<string> roomObjects;
        public string template;
    }

    [System.Serializable]
    public class User {
        public string username;
        public string avatar;
        public string designation;
        public string email;
        public string mobile;
        public List<string> rooms;
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

}

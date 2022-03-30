using UnityEngine.SceneManagement;
using UnityEngine;

namespace Scripts {
    public class GetRoomFromFirestore : MonoBehaviour
    {
        // Start is called before the first frame update
        private string roomId = "tSSPMUrsoiU6lYPGDkme";
        private string templateId;
        private Room room;
        private Template template;
        private GameObject prefab;
        public void GetRoom(){
        //    FirebaseFirestore.GetDocument("Rooms", roomId, gameObject.name, "SetRoom", "DisplayErrorObject");
        //     SetTemplate("data");

        }
        public void GetTemplate(){
            FirebaseFirestore.GetDocument("Templates", room.template, gameObject.name, "SetTemplate", "DisplayErrorObject");
            
        }
        public void SetRoom(string data){   
            room = Serializer<Room>.toObject(data);
            GetTemplate();
        }

        public void SetTemplate(string data){
            // template = Serializer<Template>.toObject(data);
            // Debug.Log(template.scene);

            // SceneManager.LoadScene(template.scene);
            SceneManager.LoadScene("Template2");
            
        }

        public void DisplayErrorObject(string error)
        {
            Debug.Log(error);
        }

        // Update is called once per frame
        void start(){
            // var path = "Prefabs/Objects/Props/Other Props/Shelf With Food";
            // prefab = Resources.Load(path) as GameObject;
            // Debug.Log(prefab);
            // Vector2 pos = new Vector2(0, 0);
            // var newWord = GameObject.Instantiate(prefab);
            // prefab.transform.position = pos;
        }
        void Update()
        {
            
        }
    }
}


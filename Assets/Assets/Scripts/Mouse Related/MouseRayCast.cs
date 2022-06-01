using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MouseRelated
{
        public class MouseRayCast : MonoBehaviour
        {
   

        public LayerMask targetLayers;
        public GameInfoBar mouseGameInfoBar; 
        public MouseClickActionScript objectHitMCAS;
        public MouseClickActionScript oldObject;

        private void Start()
        {
        
        }

        void Update()
        {
            RayCast();
            ObjectInteract();
        }

        void RayCast()
        {
            Vector3 mousePos = GetWorldPositionOnPlane(Input.mousePosition, 0f);
        
            Collider2D objectHit = Physics2D.OverlapCircle(mousePos, 0.1f, targetLayers);
        
 
            if (objectHit != null )
            {

            
                mouseGameInfoBar.SetGameInfoBarText("Left Click to Interact", mousePos);

                objectHitMCAS = objectHit.GetComponent<MouseClickActionScript>();
                if (oldObject && oldObject != objectHitMCAS)
                    oldObject.ObjectUnfocussed();
                oldObject = objectHitMCAS;
                oldObject.ObjectFocussed();
            }
            else
            {
                if (oldObject)
                {
                    oldObject.ObjectUnfocussed();
                    oldObject = null;
                    mouseGameInfoBar.ResetGameInfoBarText();
                }
            }

        }

        void ObjectInteract()
        {
            if (oldObject)
            {
                oldObject.ObjectInteract();
            }

        }


        public static Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z = 0f)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
            float distance;
            xy.Raycast(ray, out distance);
            Vector3 pos = ray.GetPoint(distance);
            pos.z = 0;
            return pos;
        }

}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;



public class InputManagerScript : MonoBehaviour
{

    public Vector2 PlayerMoveGenericController()
    {
        return GetKeyboardControlsForPlayerMovement();
    }

    public Vector2 GetKeyboardControlsForPlayerMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector2 xyInput = new(h, v);
        return xyInput;

    }
}

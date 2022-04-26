using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 250f;
 
    private Rigidbody2D ribo; 
    private PhotonView view;
    private Vector2 movement_values;

    void Start()
    {
        view = GetComponent<PhotonView>(); 
        ribo = GetComponent<Rigidbody2D>();
        movement_values = Vector2.zero;
    }

    private void Update()
    {
        if (!PhotonNetwork.IsConnected || view.IsMine)
        {
            movement_values = GetKeyboardControls();
        }
        
    }
    void FixedUpdate()
    {   
        if(!PhotonNetwork.IsConnected || view.IsMine){
            PlayerMoveByVelocity(movement_values);
        }
    }

    void PlayerMoveByVelocity(Vector2 movement){
        ribo.velocity = new Vector2(movement.x * Time.deltaTime * speed,  movement.y * Time.deltaTime * speed);
    }


    Vector2 GetKeyboardControls()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 movement = new(h, v); 
        return movement;
        
    }

}

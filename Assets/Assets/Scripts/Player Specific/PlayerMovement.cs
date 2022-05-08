using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 250f;
    public float diagonalFluidity = 2f;
    public float minVelocity = 4f;

    public InputManagerScript inputManagementScript; 

    private Rigidbody2D ribo; 
    private PhotonView view;
    private Vector2 movementXY;

    void Start()
    {

        view = GetComponent<PhotonView>(); 
        ribo = GetComponent<Rigidbody2D>();

        if(PhotonNetwork.IsConnected && !view.IsMine)           // Disable script on online players.
            this.enabled = false;
    }

    private void Update()
    {
      movementXY = inputManagementScript.PlayerMoveGenericController();
    }
    void FixedUpdate()
    {   
        PlayerMover();
        
    }

    void PlayerMover(){
        DiagonalMovementDelimeter();
        ApplyVelocityToRigidBody();
        VelocityThreshhold();
    }

    /// 
    ///  Helper Functions Below.
    /// 
    /// 
    void DiagonalMovementDelimeter()        // If you're moving vertically already, make horizontal movement slower.
    {
        movementXY.x /= (Mathf.Abs(movementXY.y) / diagonalFluidity + 1);
        movementXY.y /= (Mathf.Abs(movementXY.x) / diagonalFluidity + 1);
    }
    
    void ApplyVelocityToRigidBody()
    {
        ribo.velocity = new Vector2(movementXY.x * Time.deltaTime * speed, movementXY.y * Time.deltaTime * speed);
    }
    void VelocityThreshhold()
    {
        if (ribo.velocity.magnitude < minVelocity && ribo.velocity.magnitude > minVelocity / 2) ribo.velocity = Vector2.zero;
    }


}

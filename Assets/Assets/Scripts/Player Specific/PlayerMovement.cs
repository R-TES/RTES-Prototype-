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
    public float speedMultiplier = 1.5f;
    public Color speedBoostColor = new Color(0.8f, 1f, 0.8f, 1f);

    public LayerMask pathLayer;
    public InputManagerScript inputManagementScript; 

    private Rigidbody2D ribo; 
    private PhotonView view;
    private SpriteRenderer sr;
    private Vector2 movementXY;
    private Color defaultCharacterColor;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultCharacterColor = sr.color;
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
        float sm = SpeedModifierOnPath();
        Debug.Log(sm);
        ribo.velocity = new Vector2(movementXY.x * Time.deltaTime * speed * sm, movementXY.y * Time.deltaTime * speed * sm);
    }
    void VelocityThreshhold()
    {
        if (ribo.velocity.magnitude < minVelocity && ribo.velocity.magnitude > minVelocity / 2) ribo.velocity = Vector2.zero;
    }

    float SpeedModifierOnPath()
    {

        if (Physics2D.OverlapCircle(transform.position, 1f, pathLayer))
        {
            sr.color = speedBoostColor;
            return speedMultiplier;
        }
        sr.color = defaultCharacterColor;
        return 1; 
    }


}

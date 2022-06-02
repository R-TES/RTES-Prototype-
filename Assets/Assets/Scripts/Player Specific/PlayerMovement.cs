using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 250f;
    public float diagonalFluidity = 2f;
    public float speedMultiplier = 1.5f;
    public Color speedBoostColor = new Color(0.8f, 1f, 0.8f, 1f);

    public LayerMask pathLayer;
    public InputManagerScript inputManagementScript; 

    private Rigidbody2D ribo; 
    private PhotonView view;
    private SpriteRenderer sr;
    public Vector2 movementXY;
    private Color defaultCharacterColor;
    public bool lockDefaultInputMethod = false; 

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultCharacterColor = sr.color;
        view = GetComponent<PhotonView>(); 
        ribo = GetComponent<Rigidbody2D>();

        if(PhotonNetwork.IsConnected && !view.IsMine)           // Disable script on online players.
            this.enabled = false;

        speed *= transform.localScale.magnitude;
        
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)) lockDefaultInputMethod = true;
        else if (Input.anyKey) lockDefaultInputMethod = false;
        if(!lockDefaultInputMethod)
            movementXY = inputManagementScript.PlayerMoveGenericController(); 
    }
    void FixedUpdate()
    {
        PlayerMover(); 
    }

    void PlayerMover(){
        DiagonalMovementDelimeter();
        ApplyVelocityToRigidBody();
        
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
        ribo.velocity = new Vector2(movementXY.x * Time.deltaTime * speed * sm, movementXY.y * Time.deltaTime * speed * sm);
    }


    float SpeedModifierOnPath()
    {

        if (Physics2D.OverlapCircle(transform.position, 0.125f, pathLayer))
        {
            sr.color = speedBoostColor;
            return speedMultiplier;
        }
        //else
        sr.color = defaultCharacterColor;
        return 1; 
    }

    public void ApplyMotion(Vector2 xy)
    {
        movementXY = xy;
    }

}

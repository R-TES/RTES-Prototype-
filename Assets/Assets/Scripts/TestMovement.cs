using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{ 
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    
    private void Awake()
    {   
        //Grabs references for rigidbody and animator from game object.
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
 
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
 
        //Flip player when facing left/right.
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
 
        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();
 
        //sets animation parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }
 
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        grounded = false;
    }
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }
}

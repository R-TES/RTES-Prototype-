using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    //public float force = 0.01f;
    //public float animate_threshold = 0.001f;
    private Animator animator; 
    private Rigidbody2D ribo; 
    private PhotonView view;

    private Vector3 vel;
    private Vector3 previous = Vector3.zero; 

    
    // Start is called before the first frame update


    void Start()
    {
        view = GetComponent<PhotonView>(); 
        ribo = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        //if(view.IsMine){
            PlayerMove();
            AnimatePlayer();
        //}
    }


    void PlayerMove(){

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        ribo.velocity = new Vector2(h * Time.deltaTime * speed,  v * Time.deltaTime * speed);

    }

    void AnimatePlayer(){
        ResetAnim();

        vel = (transform.position - previous);
        previous = transform.position;

 
        if(vel.x < 0){        // Left Key Pressed
            
            animator.SetInteger("dir", 1);
            //animator.SetBool("LEFTRUN", true);
        }
        else if(vel.x > 0){
            
            animator.SetInteger("dir", 3);
        }
        else if(vel.y > 0){
            
            animator.SetInteger("dir", 2);
        } 
        else if(vel.y < 0){
            
            animator.SetInteger("dir", 0);
        }  
        else{
            
            animator.SetInteger("dir", -1);
        }
    }

    void ResetAnim(){
        animator.SetInteger("dir", -1);
    }

}

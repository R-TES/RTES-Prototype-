using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public float force = 0.01f;
    public float animate_threshold = 0.001f;
    public Animator animator; 
    public Rigidbody2D ribo; 

    private Vector3 vel;
    private Vector3 previous = Vector3.zero; 

    public PhotonView view; 
    // Start is called before the first frame update


    void Start()
    {
        view = GetComponent<PhotonView>(); 
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
        transform.position += new Vector3(h * Time.deltaTime * speed,  v * Time.deltaTime * speed, 0);



        if(h > 0){
            ribo.AddForce(transform.right * force, ForceMode2D.Impulse);
        }
        else if(h < 0){
            ribo.AddForce(-transform.right * force, ForceMode2D.Impulse);
        }

        if(v > 0){
            ribo.AddForce(transform.up * force, ForceMode2D.Impulse);
        }
        else if(v < 0){
            ribo.AddForce(-transform.up * force, ForceMode2D.Impulse);
        }
        



    }

    void AnimatePlayer(){
        ResetAnim();

        vel = (transform.position - previous);
        previous = transform.position;

 
        if(vel.x < -animate_threshold){        // Left Key Pressed
            //ResetAnim();
            animator.SetBool("IDLE", false);
            animator.SetBool("LEFTRUN", true);
        }
        else if(vel.x > animate_threshold){
            //ResetAnim();
            animator.SetBool("IDLE", false);
            animator.SetBool("RIGHTRUN", true);
        }
        else if(vel.y > animate_threshold){
            //ResetAnim();
            animator.SetBool("IDLE", false);
            animator.SetBool("UPRUN", true);
        } 
        else if(vel.y < -animate_threshold){
            //ResetAnim();
            animator.SetBool("IDLE", false);
            animator.SetBool("DOWNRUN", true);
        }  
        else{
            //ResetAnim();
            animator.SetBool("IDLE", true);
        }
    }

    void ResetAnim(){
            animator.SetBool("DOWNRUN", false);
            animator.SetBool("UPRUN", false);
            animator.SetBool("RIGHTRUN", false);
            animator.SetBool("LEFTRUN", false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 250;

    private Animator animator; 
    private Rigidbody2D ribo; 
    private PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>(); 
        ribo = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {   
        if(!PhotonNetwork.IsConnected || view.IsMine){
            PlayerMove();
            AnimatePlayer();
        }
    }

    void PlayerMove(){
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        ribo.velocity = new Vector2(h * Time.deltaTime * speed,  v * Time.deltaTime * speed);
    }

    void AnimatePlayer(){
 
        if(ribo.velocity.x < 0){        // Left Key Pressed
            animator.SetInteger("dir", 1);
        }
        else if(ribo.velocity.x > 0){
            animator.SetInteger("dir", 3);
        }
        else if(ribo.velocity.y > 0){  // Up Key Pressed
            animator.SetInteger("dir", 2);
        } 
        else if(ribo.velocity.y < 0){
            animator.SetInteger("dir", 0);
        }  
        else{
            animator.SetInteger("dir", -1);
        }
    }

}

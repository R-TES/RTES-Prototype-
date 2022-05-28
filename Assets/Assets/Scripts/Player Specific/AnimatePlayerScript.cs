using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayerScript : MonoBehaviour
{
    public string directionStateVariable = "dir";
    public string movingStateVaraible = "isMoving";
    public float velocityThreshhold = 0.5f;

    private enum Direction:int{Down, Right, Up, Left};

    private Rigidbody2D ribo;
    private Animator animator;
    void Start()
    {
        ribo = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        AnimatePlayer();
    }

    // Update is called once per frame
    void AnimatePlayer()
    {
        if (ribo.velocity == Vector2.zero)
        {
            IdleAnimation(true);
        }
        else if (ribo.velocity.x < -velocityThreshhold)
        {                                           
            RunningAnimation(Direction.Left);
        }
        else if (ribo.velocity.x > velocityThreshhold)
        {
            RunningAnimation(Direction.Right);
        }
        else if (ribo.velocity.y > velocityThreshhold)
        {                                           
            RunningAnimation(Direction.Up);
        }
        else if (ribo.velocity.y < -velocityThreshhold)
        {
            RunningAnimation(Direction.Down);
        }

    }

    private void IdleAnimation(bool state=true)
    {
        animator.SetBool("isMoving", !state);
        animator.speed = 1;
    }
    
    private void RunningAnimation(Direction dir)
    {
        IdleAnimation(false);
        animator.SetInteger(directionStateVariable, (int)dir);
        animator.speed = ribo.velocity.magnitude/6; 
        
    }

}

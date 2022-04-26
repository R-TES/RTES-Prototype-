using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayerScript : MonoBehaviour
{
    public string directionStateVariable = "dir";
    public string movingStateVaraible = "isMoving";

    private enum Direction:int{Down, Right, Up, Left};

    private Rigidbody2D ribo;
    private Animator animator;
    void Start()
    {
        ribo = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
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
        else if (ribo.velocity.x < -1)
        {                                           
            RunningAnimation(Direction.Left);
        }
        else if (ribo.velocity.x > 1)
        {
            RunningAnimation(Direction.Right);
        }
        else if (ribo.velocity.y > 1)
        {                                           
            RunningAnimation(Direction.Up);
        }
        else if (ribo.velocity.y < -1)
        {
            RunningAnimation(Direction.Down);
        }

    }

    private void IdleAnimation(bool state=true)
    {
        animator.SetBool("isMoving", !state);
    }
    
    private void RunningAnimation(Direction dir)
    {
        IdleAnimation(false);
        animator.SetInteger(directionStateVariable, (int)dir);
        
    }

}

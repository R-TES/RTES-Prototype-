using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateObjectIfNear : MonoBehaviour
{
    public string animation_variable = "active";
    private Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        animator.SetBool(animation_variable, true);

    }

    void OnTriggerExit2D(Collider2D col)
    {
        animator.SetBool(animation_variable, false);
    }



}

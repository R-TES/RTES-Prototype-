using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateObjectAnimationIfNear : MonoBehaviour
{
    public string animation_variable = "active";
    public bool disabled = false;
    private Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("IgnoreTriggers")) return;

        if (!disabled)
            TriggerActivateObjectAnimation();
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("IgnoreTriggers")) return;

        if (!disabled)
            TriggerDeactivateObjectAnimation();
    }


    public void DisableObject(){
        disabled = true;
       
    }

    public void EnableObject()
    {
        disabled = false;
    }

    public void TriggerActivateObjectAnimation()
    {
        animator.SetBool(animation_variable, true);
    }

    public void TriggerDeactivateObjectAnimation()
    {
        animator.SetBool(animation_variable, false);
    }

}

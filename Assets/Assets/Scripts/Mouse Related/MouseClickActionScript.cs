using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseClickActionScript : MonoBehaviour
{

    public Color hoverColor = new(1f, 0.6f, 0.6f, 1);

    public UnityEvent leftClickEvent;
    public UnityEvent rightClickEvent;
    public UnityEvent middleClickEvent;


    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }
    
    void OnMouseEnter()
    {
        ObjectFocussed();
    }

    private void OnMouseExit()
    {
        ObjectUnfocussed();
    }

    public void ObjectFocussed()
    {
        spriteRenderer.color = hoverColor;
    }

    public void ObjectUnfocussed()
    {
        spriteRenderer.color = originalColor;
    }

    private void OnMouseOver()
    {
        ObjectInteract();
    }

    public void ObjectInteract() {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed left click.");
            leftClickEvent.Invoke();
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Pressed right click.");
            rightClickEvent.Invoke();
        }
        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("Pressed middle click.");
            middleClickEvent.Invoke();
        }
    }

    
    public void DebugFunction()
    {
        Debug.Log("Works");
    }
}

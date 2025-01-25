using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShakeCup : MonoBehaviour
{
    [SerializeField] private EdgeCollider2D cupTopCollider;
    [SerializeField] private float cupDragForce = 100;
    
    private Camera cam;
    private Rigidbody2D rb;
    
    private bool dragging;
    
    // Start is called before the first frame update
    void Start()
    {
        cupTopCollider.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        
        cam = Camera.main;
    }

    private void FixedUpdate()
    {
        // shake logic
        if (dragging && cam)
        {
            Vector3 newPos = cam.ScreenToWorldPoint(Input.mousePosition);
            newPos.z = 0;
            Debug.Log((newPos - transform.position).magnitude);
            
            // set direction
            Vector2 direction = (newPos - transform.position).normalized;
            
            // check if too close to last position
            if ((newPos - transform.position).magnitude > 0.5f)
            {
                // apply force
                rb.velocity = Vector2.zero;
                rb.AddForceAtPosition(direction * (cupDragForce * 10), newPos, ForceMode2D.Impulse);
            }
            else if ((newPos - transform.position).magnitude > 0.05f)
            {
                // apply littler force for smoothing
                rb.velocity = Vector2.zero;
                rb.AddForceAtPosition(direction * cupDragForce, newPos, ForceMode2D.Impulse);
            }
            else
            {
                // force dead zone
                rb.velocity = Vector2.zero;
            }
            
            // reset cursor to center of cup (trust me it somehow makes it feel better)
            Mouse.current.WarpCursorPosition(cam.WorldToScreenPoint(transform.position));
        }
    }


    private void OnMouseDown()
    {
        dragging = true;
        
        // hide cursor
        Cursor.visible = false;
        
        // temp
        cupTopCollider.enabled = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        
        // show cursor
        Cursor.visible = true;
        
        // temp
        cupTopCollider.enabled = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShakeCup : MonoBehaviour
{
    [SerializeField] private Transform cupSlot;
    [SerializeField] private EdgeCollider2D cupTopCollider;
    [SerializeField] private float cupDragForce = 100;
    
    private Camera cam;
    private Rigidbody2D rb;
    
    private bool dragging;
    [SerializeField] private float resetSpeed = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        cupTopCollider.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        
        transform.position = cupSlot.position;
    }

    private void FixedUpdate()
    {
        // shake logic
        if (dragging && cam)
        {
            Vector3 newPos = cam.ScreenToWorldPoint(Input.mousePosition);
            newPos.z = 0;
            
            // set direction
            Vector2 direction = (newPos - transform.position).normalized;
            
            // check if too close to last position
            if ((newPos - transform.position).magnitude > 0.5f)
            {
                // apply force
                rb.velocity = Vector2.zero;
                rb.AddForceAtPosition(direction * (cupDragForce * 5), newPos, ForceMode2D.Impulse);
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
        
        // reset cup position
        if (!dragging)
        {
            // reset position back to cup slot
            rb.velocity = Vector2.zero;
            Vector2 resetDirection = cupSlot.position - transform.position;

            if (resetDirection.magnitude < 0.01)
            {
                // lock position
                transform.position = cupSlot.position;
                
                // unlock cup top
                cupTopCollider.enabled = false;
            }
            else
            {
                // move to cup slot
                rb.AddForce(resetDirection.normalized * (resetDirection.magnitude * resetSpeed), ForceMode2D.Impulse);
            }
            
            // reset rotation back to cup slot
            if (Mathf.Abs(transform.rotation.eulerAngles.z) < 1)
            {
                // lock rotation
                rb.angularVelocity = 0;
                rb.totalTorque = 0;
                transform.rotation = quaternion.identity;
            }
            
        }
    }
    
    private void OnMouseDown()
    {
        if (GameManager.Instance.currentStation == STATION_TYPE.SHAKE)
        {

            dragging = true;

            // hide cursor
            Cursor.visible = false;

            // temp
            cupTopCollider.enabled = true;

        }
    }

    private void OnMouseUp()
    {
        if (GameManager.Instance.currentStation == STATION_TYPE.SHAKE)
        {
            dragging = false;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;

            // show cursor
            Cursor.visible = true;

            // apply return rotation
            if (transform.rotation.eulerAngles.z > 0)
            {
                // rotate to 0 CW or ACW, no idea
                rb.totalTorque = -1000;
            }
            else if (transform.rotation.eulerAngles.z < 0)
            {
                // rotate to 0 CW or ACW, no idea
                rb.totalTorque = 1000;
            }
        }
    }
}

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
    
    private Camera cam;
    private Rigidbody2D rb;
    
    private bool dragging;
    private float releaseTimer;

    private Vector2 prevPos;
    
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
            Vector2 targetPos = cam.ScreenToWorldPoint(Input.mousePosition);

            rb.MovePosition(targetPos);
        }
        
        // reset cup
        if (!dragging)
        {
            // fling cup
            if (releaseTimer > 1)
            {
                Vector2 releaseVelocity = ((Vector2)transform.position - prevPos) * 50;
                rb.velocity = Vector2.Lerp(Vector2.zero, releaseVelocity, (releaseTimer - 1) / 0.2f);
                
                releaseTimer -= Time.fixedDeltaTime;
            }
            else
            {
                rb.MovePosition(cupSlot.position);
            }
            
            // take off lid
            if (releaseTimer <= 0)
            {
                cupTopCollider.enabled = false;
            }
        }
        
        prevPos = transform.position;
    }
    
    private void OnMouseDown()
    {
        if (GameManager.Instance.currentStation == STATION_TYPE.SHAKE)
        {
            dragging = true;

            // hide cursor
            Cursor.visible = false;
            
            cupTopCollider.enabled = true;

            releaseTimer = 1.2f;
        }
    }

    private void OnMouseUp()
    {
        if (GameManager.Instance.currentStation == STATION_TYPE.SHAKE)
        {
            dragging = false;

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

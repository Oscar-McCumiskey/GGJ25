using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        // Destroy this tapioca
        if (other.gameObject.CompareTag("Tapioca"))
        {
            Destroy(other.gameObject);
        }
        
        // record with score manager
        // GameManager.Instance.tapiocaOverflowed += 1
    }
}

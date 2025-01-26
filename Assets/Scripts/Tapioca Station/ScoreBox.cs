using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tapioca"))
        {
            // Change score
            if (other.gameObject.GetComponent<Tapioca>().getTapiocaType() == OrderManager.Instance.currentOrder.tapiocaType)
            {
                ScoreManager.Instance.AddScore(50);
            }
        }
    }
}

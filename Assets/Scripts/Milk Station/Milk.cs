using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milk : MonoBehaviour
{
    public Transform milkEndPoint;
    private float milkSpeed = 100;
    private bool milkTriggered = false;

    public void IncreaseMilk()
    {
        if(!milkTriggered)
        {
            milkTriggered = true;
            StartCoroutine(IncreaseMilkHeight());
        }
    }

    IEnumerator IncreaseMilkHeight()
    {
        while (transform.position.y < (milkEndPoint.transform.position.y))
        {
            if (!MilkDispenser.Instance.hitBottom)
            {
                break;
            }

            transform.position = new Vector3(transform.position.x, transform.position.y + milkSpeed * Time.deltaTime, transform.position.z);
            yield return null;
        }

        //reset 
        milkTriggered = false;
        yield return null;
    }
}

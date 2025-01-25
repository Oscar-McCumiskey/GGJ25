using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MilkDispenser : MonoBehaviour
{
    public static MilkDispenser Instance;

    [SerializeField] private Transform milkSpawnLocation;
    [SerializeField] private Transform milkEndPoint;
    [SerializeField] private Transform milk;
    [SerializeField] private float milkSpeed;
    [SerializeField] private ButtonHeld buttonScript;
    private bool milkPouring = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    private void Update()
    {
        if(buttonScript.isButtonDown)
        {
            Dispense();
        }
    }

    /// <summary>
    /// starts dispenses milk
    /// </summary>
    public void Dispense()
    {
        Debug.Log("milk start y: " + milk.transform.position.y);
        Debug.Log("milk end y: " + milkEndPoint.transform.position.y);  

        if(!milkPouring)
        {
            milkPouring = true;
            StartCoroutine(lowerMilk());
        }
        
    }

    IEnumerator lowerMilk()
    {
        while(milk.transform.position.y > (milkEndPoint.transform.position.y))
        {
            if(!buttonScript.isButtonDown)
            {
                milkPouring = false;
                break;
            }

            milk.transform.position = new Vector3(milk.transform.position.x, milk.transform.position.y - milkSpeed * Time.deltaTime, milk.transform.position.z);
            yield return null;
        }

        milkPouring = false;
        yield return null;
    }
}

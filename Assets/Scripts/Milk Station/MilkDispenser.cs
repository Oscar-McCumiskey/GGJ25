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
    public bool hitBottom = false;

    [SerializeField] private Sprite mangoMilk;
    [SerializeField] private Sprite strawberryMilk;
    [SerializeField] private Sprite chcocolateMilk;
    [SerializeField] private Sprite MatchMilk;
    [SerializeField] private Sprite mangoMilkStream;
    [SerializeField] private Sprite strawberryMilkStream;
    [SerializeField] private Sprite chcocolateMilkStream;
    [SerializeField] private Sprite MatchMilkStream;

    float maxFillTime = 0;
    float currentFillTime = 0;
    float overFillTime = 0;

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

    private void Start()
    {
        maxFillTime = 3f;
        overFillTime = 6f;
        currentFillTime = 0;
    }

    private void Update()
    {
        if(buttonScript.isButtonDown)
        {
            //Dispenses milk
            Dispense();

            //check overfilled
            if(currentFillTime >= overFillTime)
            {
                Debug.Log("TOO FILLED");
            } 

        }
        else
        {
            hitBottom = false;
            ResetMilk();
        }

        if (hitBottom)
        {
            //update fill amount with time 
            currentFillTime += Time.deltaTime;

            Debug.Log("HIT BOTTOM");
            GameManager.Instance.currentCup.transform.GetChild(0).gameObject.SetActive(true);
            GameManager.Instance.currentCup.transform.GetChild(0).GetComponent<Milk>().IncreaseMilk();
        }
    }

    /// <summary>
    /// starts dispenses milk
    /// </summary>
    public void Dispense()
    {
        //pour milk only if not already pouring
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

            if (currentFillTime >= overFillTime)
            {
                milkPouring = false;
                break;
            }

            milk.transform.position = new Vector3(milk.transform.position.x, milk.transform.position.y - milkSpeed * Time.deltaTime, milk.transform.position.z);
            yield return null;
        }

        //reset 
        hitBottom = true;
        milkPouring = false;
        yield return null;
    }

    private void ResetMilk()
    {
        milk.transform.position = milkSpawnLocation.transform.position;
    }
}

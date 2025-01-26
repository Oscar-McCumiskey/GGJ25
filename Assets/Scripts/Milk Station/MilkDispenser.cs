using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MilkDispenser : MonoBehaviour
{
    public static MilkDispenser Instance;

    [SerializeField] private Transform milkSpawnLocation;
    [SerializeField] private Transform milkEndPoint;
    [SerializeField] private Transform milkStream;
    [SerializeField] private Transform milkStreamGO;
    [SerializeField] private float milkSpeed;
    [SerializeField] private ButtonHeld buttonScript;
    private bool milkPouring = false;
    public bool hitBottom = false;

    [SerializeField] private Sprite mangoMilk;
    [SerializeField] private Sprite strawberryMilk;
    [SerializeField] private Sprite chocolateMilk;
    [SerializeField] private Sprite matchaMilk;
    [SerializeField] private Sprite mangoMilkStream;
    [SerializeField] private Sprite strawberryMilkStream;
    [SerializeField] private Sprite chocolateMilkStream;
    [SerializeField] private Sprite matchaMilkStream;

    float maxFillTime = 0;
    float currentFillTime = 0;
    float overFillTime = 0;

    private void Awake()
    {
        if (Instance == null)
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
        if (buttonScript.isButtonDown)
        {
            //Dispenses milk
            Dispense();

            //check overfilled
            if (currentFillTime >= overFillTime)
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
        if (!milkPouring)
        {
            milkPouring = true;
            StartCoroutine(lowerMilk());
        }
    }

    IEnumerator lowerMilk()
    {
        while (milkStream.transform.position.y > (milkEndPoint.transform.position.y))
        {
            if (!buttonScript.isButtonDown)
            {
                milkPouring = false;
                break;
            }

            if (currentFillTime >= overFillTime)
            {
                milkPouring = false;
                break;
            }

            milkStream.transform.position = new Vector3(milkStream.transform.position.x, milkStream.transform.position.y - milkSpeed * Time.deltaTime, milkStream.transform.position.z);
            yield return null;
        }

        //reset 
        hitBottom = true;
        milkPouring = false;
        yield return null;
    }

    private void ResetMilk()
    {
        milkStream.transform.position = milkSpawnLocation.transform.position;
    }

    public void SetStrawberryMilk()
    {
        milkStreamGO.GetComponent<SpriteRenderer>().sprite = strawberryMilkStream;
    }

    public void SetMangoMilk()
    {
        milkStreamGO.GetComponent<SpriteRenderer>().sprite = mangoMilkStream;
    }

    public void SetMatchaMilk()
    {
        milkStreamGO.GetComponent<SpriteRenderer>().sprite = matchaMilkStream;
    }

    public void SetChocolateMilk()
    {
        milkStreamGO.GetComponent<SpriteRenderer>().sprite = chocolateMilkStream;
    }
}

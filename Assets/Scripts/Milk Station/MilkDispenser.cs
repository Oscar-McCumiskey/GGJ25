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
    [SerializeField] private ButtonHeldMango mangoButtonScript;
    [SerializeField] private ButtonHeldChocolate chocolateButtonScript;
    [SerializeField] private ButtonHeldMatcha matchaButtonScript;
    [SerializeField] private ButtonHeldStrawberry strawberryButtonScript;
    private bool milkPouring = false;
    public bool hitBottom = false;
    public bool holding = false;

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
        //if any button is active
        if(mangoButtonScript.isButtonDown || matchaButtonScript.isButtonDown || strawberryButtonScript.isButtonDown || chocolateButtonScript.isButtonDown)
        {
            holding = true;

            if (mangoButtonScript.isButtonDown && OrderManager.Instance.currentOrder.selectedMilkType != MILK_TYPE.MANGO)
            {
                holding = false;
            }
            if (matchaButtonScript.isButtonDown && OrderManager.Instance.currentOrder.selectedMilkType != MILK_TYPE.MATCHA)
            {
                holding = false;
            }
            if (strawberryButtonScript.isButtonDown && OrderManager.Instance.currentOrder.selectedMilkType != MILK_TYPE.STRAWBERRY)
            {
                holding = false;
            }
            if (chocolateButtonScript.isButtonDown && OrderManager.Instance.currentOrder.selectedMilkType != MILK_TYPE.CHOCOLATE)
            {
                holding = false;
            }
        }
        else
        {
            holding = false;
        }

        if (holding && OrderManager.Instance.currentOrder.isThereACup)
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
        while (milkStream.transform.position.y > milkEndPoint.transform.position.y + 500)
        {
            if (!holding)
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
        if (OrderManager.Instance.currentOrder.selectedMilkType != MILK_TYPE.NONE || !OrderManager.Instance.currentOrder.isThereACup)
        {
            return;
        }

        if (currentFillTime >= overFillTime) return;
        milkStreamGO.GetComponent<SpriteRenderer>().sprite = strawberryMilkStream;
        GameManager.Instance.currentCup.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = strawberryMilk;
        OrderManager.Instance.currentOrder.selectedMilkType = MILK_TYPE.STRAWBERRY;
    }

    public void SetMangoMilk()
    {
        if (OrderManager.Instance.currentOrder.selectedMilkType != MILK_TYPE.NONE || !OrderManager.Instance.currentOrder.isThereACup)
        {
            return;
        }

        if (currentFillTime >= overFillTime) return;
        milkStreamGO.GetComponent<SpriteRenderer>().sprite = mangoMilkStream;
        GameManager.Instance.currentCup.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = mangoMilk;
        OrderManager.Instance.currentOrder.selectedMilkType = MILK_TYPE.MANGO;
    }

    public void SetMatchaMilk()
    {
        if (OrderManager.Instance.currentOrder.selectedMilkType != MILK_TYPE.NONE || !OrderManager.Instance.currentOrder.isThereACup)
        {
            return;
        }

        if (currentFillTime >= overFillTime) return;
        milkStreamGO.GetComponent<SpriteRenderer>().sprite = matchaMilkStream;
        GameManager.Instance.currentCup.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = matchaMilk;
        OrderManager.Instance.currentOrder.selectedMilkType = MILK_TYPE.MATCHA;
    }

    public void SetChocolateMilk()
    {
        if (OrderManager.Instance.currentOrder.selectedMilkType != MILK_TYPE.NONE || !OrderManager.Instance.currentOrder.isThereACup)
        {
            return;
        }

        if (currentFillTime >= overFillTime) return;
        milkStreamGO.GetComponent<SpriteRenderer>().sprite = chocolateMilkStream;
        GameManager.Instance.currentCup.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = chocolateMilk;
        OrderManager.Instance.currentOrder.selectedMilkType = MILK_TYPE.CHOCOLATE;
    }
}

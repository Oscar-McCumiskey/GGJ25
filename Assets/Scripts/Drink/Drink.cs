using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// information class with drink data
/// </summary>
public class Drink : MonoBehaviour
{
    //Drink details
    public CUP_SIZE cupSize = CUP_SIZE.NONE;
    public TAPIOCA_TYPE tapiocaType = TAPIOCA_TYPE.NONE;
    public MILK_TYPE milkType = MILK_TYPE.NONE;

    //to find cup ref

    //Drink checks - we need this to judge score
    bool correctTapioca = false;
    bool correctCupSize = false;
    bool isThereACup = false;
    bool correctMilkType = false;
    float milkQuantityPercentage;
    private float timeScore = 1000; //order time taken to calcualte score

    public void Reset()
    {
        cupSize = CUP_SIZE.NONE;
        tapiocaType = TAPIOCA_TYPE.NONE;
        milkType = MILK_TYPE.NONE;
    }

    public void RandomizeDrink()
    {
        //cup size
        int valueCount = System.Enum.GetValues(typeof(CUP_SIZE)).Length;
        cupSize = (CUP_SIZE)Random.Range(1, valueCount);

        //tapioca type
        valueCount = System.Enum.GetValues(typeof(TAPIOCA_TYPE)).Length;
        tapiocaType = (TAPIOCA_TYPE)Random.Range(1, valueCount);

        //milk type
        valueCount = System.Enum.GetValues(typeof(MILK_TYPE)).Length;
        milkType = (MILK_TYPE)Random.Range(1, valueCount);
    }

    /// Functions that check the drink correctness - - - - - - - - - - - - - - -
    public void CheckCupSize(CUP_SIZE cupSizeInput)
    {
        isThereACup = true;
        correctCupSize = cupSizeInput == cupSize;

        if (cupSizeInput != CUP_SIZE.NONE)
        {
            if(cupSizeInput == CUP_SIZE.SMALL)
            {
                GameManager.Instance.currentCup = GameManager.Instance.smallCup;
            }
            else if(cupSizeInput == CUP_SIZE.MEDIUM)
            {
                GameManager.Instance.currentCup = GameManager.Instance.mediumCup;
            }
            else
            {
                GameManager.Instance.currentCup = GameManager.Instance.largeCup;
            }

        }

    }

    public void CheckTapiocaType(TAPIOCA_TYPE tapiocaTypeInput)
    {
        //confirm there is atleast a cup
        if(!isThereACup)
        {
            return;
        }

        correctTapioca = tapiocaTypeInput == tapiocaType;
    }

    public void CheckMilkType(MILK_TYPE milkTypeInput)
    {
        correctMilkType = milkTypeInput == milkType;
    }
}

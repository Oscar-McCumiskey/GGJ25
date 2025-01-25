using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// information class with drink data
/// </summary>
public class Drink : MonoBehaviour
{
    public CUP_SIZE cupSize = CUP_SIZE.NONE;
    public TAPIOCA_TYPE tapiocaType = TAPIOCA_TYPE.NONE;
    public MILK_TYPE milkType = MILK_TYPE.NONE;

    bool correctTapioca = false;
    bool correctCupSize = false;
    bool correctMilkType = false;
    
    //quantities
    float milkQuantityPercentage;

    //order time taken to calcualte score
    private float timeScore = 1000;

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
        cupSize = (CUP_SIZE)Random.Range(0, valueCount);

        //tapioca type
        valueCount = System.Enum.GetValues(typeof(TAPIOCA_TYPE)).Length;
        tapiocaType = (TAPIOCA_TYPE)Random.Range(0, valueCount);

        //milk type
        valueCount = System.Enum.GetValues(typeof(MILK_TYPE)).Length;
        milkType = (MILK_TYPE)Random.Range(0, valueCount);
    }
}

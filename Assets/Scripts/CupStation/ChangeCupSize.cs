using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCupSize : MonoBehaviour
{
    [SerializeField] private GameObject smallCup;
    [SerializeField] private GameObject mediumCup;
    [SerializeField] private GameObject largeCup;
    private List<GameObject> cupSizeList = new List<GameObject>();
    private int currentCupIndex = 0;

    private void Start()
    {
        cupSizeList.Add(smallCup);
        cupSizeList.Add(mediumCup);
        cupSizeList.Add(largeCup);

        //initial cup size (medium)
        currentCupIndex = 1;
        cupSizeList[currentCupIndex].SetActive(true);
    }
    public void SmallerCupSize()
    {
        int oldCupIndex = currentCupIndex;
        currentCupIndex--;

        if(currentCupIndex < 0)
        {
            currentCupIndex = cupSizeList.Count - 1;
        }

        cupSizeList[oldCupIndex].SetActive(false);
        cupSizeList[currentCupIndex].SetActive(true);
    }

    public void LargerCupSize()
    {
        int oldCupIndex = currentCupIndex;
        currentCupIndex++;

        if (currentCupIndex > cupSizeList.Count - 1)
        {
            currentCupIndex = 0;
        }

        cupSizeList[oldCupIndex].SetActive(false);
        cupSizeList[currentCupIndex].SetActive(true);
    }
}

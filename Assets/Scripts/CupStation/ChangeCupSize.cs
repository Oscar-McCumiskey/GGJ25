using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCupSize : MonoBehaviour
{
    [SerializeField] private GameObject smallCup;
    [SerializeField] private GameObject mediumCup;
    [SerializeField] private GameObject largeCup;

    private void Start()
    {
        //initial cup size (medium)
        mediumCup.SetActive(true);
    }
    public void SmallCup()
    {
        smallCup.SetActive(true);

        mediumCup.SetActive(false);
        largeCup.SetActive(false);
    }

    public void MediumCup()
    {
        mediumCup.SetActive(true);

        smallCup.SetActive(false);
        largeCup.SetActive(false);
    }

    public void LargeCup()
    {
        largeCup.SetActive(true);

        smallCup.SetActive(false);
        mediumCup.SetActive(false);
    }
}

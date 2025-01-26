using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeCupSize : MonoBehaviour
{
    [SerializeField] private GameObject smallCup;
    [SerializeField] private GameObject mediumCup;
    [SerializeField] private GameObject largeCup;
    [SerializeField] private Transform cupSpawnPoint;
    [SerializeField] private Transform cupEndPoint;
    private Transform currentCup = null;
    private void Start()
    {
        //initial cup size (medium)
        smallCup.SetActive(false);
        mediumCup.SetActive(false);
        largeCup.SetActive(false);
        
        smallCup.transform.position = cupSpawnPoint.transform.position;
        mediumCup.transform.position = cupSpawnPoint.transform.position;
        largeCup.transform.position = cupSpawnPoint.transform.position;
    }
    public void SmallCup()
    {
        smallCup.SetActive(true);
        smallCup.transform.position = cupSpawnPoint.transform.position;
        currentCup = smallCup.transform;

        mediumCup.SetActive(false);
        largeCup.SetActive(false);

        StartCoroutine(MoveCupTowardsEndPosition());

        OrderManager.Instance.currentOrder.CheckCupSize(CUP_SIZE.SMALL);
    }

    public void MediumCup()
    {
        mediumCup.SetActive(true);
        mediumCup.transform.position = cupSpawnPoint.transform.position;
        currentCup = mediumCup.transform;

        smallCup.SetActive(false);
        largeCup.SetActive(false);

        StartCoroutine(MoveCupTowardsEndPosition());

        OrderManager.Instance.currentOrder.CheckCupSize(CUP_SIZE.MEDIUM);
    }

    public void LargeCup()
    {
        largeCup.SetActive(true);
        largeCup.transform.position = cupSpawnPoint.transform.position;
        currentCup = largeCup.transform;

        smallCup.SetActive(false);
        mediumCup.SetActive(false);

        StartCoroutine(MoveCupTowardsEndPosition());

        OrderManager.Instance.currentOrder.CheckCupSize(CUP_SIZE.LARGE);
    }

    IEnumerator MoveCupTowardsEndPosition()
    {
        while(currentCup.position.x < cupEndPoint.position.x)
        {
            currentCup.position = Vector3.MoveTowards(currentCup.position, cupEndPoint.position, 600f * Time.deltaTime);

            yield return null;
        }

        yield return null;
    }
}

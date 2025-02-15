using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] Canvas[] stationList;
    
    public STATION_TYPE currentStation = STATION_TYPE.CUP;

    public GameObject currentCup;

    public GameObject largeCup;
    public GameObject mediumCup;
    public GameObject smallCup;

    private List<Vector3> newCanvasPosition = new();
    private List<Vector3> prevCanvasPosition = new();
    private bool isMoving = false;
    private float moveTimer = 0;
    private float moveTime = 0.5f;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        currentCup = null;
        StartGame();
        
        moveTimer = moveTime;
    }

    private void Update()
    {
        if (isMoving)
        {
            for (int i = 0; i < stationList.Length; i++)
            {
                stationList[i].GetComponent<RectTransform>().position = Vector3.Lerp(newCanvasPosition[i], prevCanvasPosition[i], moveTimer / moveTime);
            }

            // stop moving
            if (moveTimer <= 0)
            {
                moveTimer = moveTime;
                isMoving = false;
            }
            
            moveTimer -= Time.deltaTime;
        }
    }

    public void StartGame()
    {
        //Initialise the orders
        OrderManager.Instance.Init();
    }

    public void NextStation()
    {
        if (!isMoving)
        {
            if ((int)currentStation == stationList.Length - 1) return;
            
            prevCanvasPosition.Clear();
            newCanvasPosition.Clear();
            
            for (int i = 0; i < stationList.Length; i++)
            {
                //canvas.GetComponent<RectTransform>().position += new Vector3(-1920f, 0f, 0f);
                prevCanvasPosition.Add(stationList[i].transform.position);
                newCanvasPosition.Add(stationList[i].transform.position + new Vector3(-1920f, 0f, 0f));
            }

            currentStation++;
            isMoving = true;
        }
    }

    public void PrevStation()
    {
        if (!isMoving)
        {
            if ((int)currentStation - 1 == -1) return;
            
            prevCanvasPosition.Clear();
            newCanvasPosition.Clear();
            
            for (int i = 0; i < stationList.Length; i++)
            {
                //canvas.GetComponent<RectTransform>().position += new Vector3(1920f, 0f, 0f);
                prevCanvasPosition.Add(stationList[i].transform.position);
                newCanvasPosition.Add(stationList[i].transform.position + new Vector3(1920f, 0f, 0f));
            }

            currentStation--;
            isMoving = true;
        }
    }

    public void Reset()
    {
        currentCup = null;
    }
}

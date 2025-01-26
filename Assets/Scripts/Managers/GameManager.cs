using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] Canvas[] stationList;
    
    public STATION_TYPE currentStation = STATION_TYPE.CUP;

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
        StartGame();
    }

    public void StartGame()
    {
        //Initialise the orders
        OrderManager.Instance.Init();
    }

    public void NextStation()
    {
        if ((int)currentStation == stationList.Length - 1) return;
        foreach (Canvas canvas in stationList)
        {
            canvas.GetComponent<RectTransform>().position += new Vector3(-1920f, 0f, 0f);
        }
        currentStation++;
    }

    public void PrevStation()
    {
        if ((int)currentStation - 1 == -1) return;
        foreach (Canvas canvas in stationList)
        {
            canvas.GetComponent<RectTransform>().position += new Vector3(1920f, 0f, 0f);
        }
        currentStation--;
    }
}

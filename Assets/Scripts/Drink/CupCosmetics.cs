using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupCosmetics : MonoBehaviour
{
    [SerializeField] private GameObject straw;
    [SerializeField] private GameObject lid;
    [SerializeField] private float animTime = 0.5f;

    private float lidTimer;
    private float strawTimer;

    private Vector2 lidPos;
    private Vector2 strawPos;

    private STATION_TYPE prevStation;
    private bool wasPrevStationServe = false;
    
    // Start is called before the first frame update
    void Start()
    {
        lidPos = lid.transform.localPosition;
        strawPos = straw.transform.localPosition;
        
        lid.SetActive(false);
        straw.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // check if station changed
        if (prevStation != GameManager.Instance.currentStation)
        {
            strawTimer = animTime;
            lidTimer = animTime;
        }
        
        // enable lid
        if (GameManager.Instance.currentStation == STATION_TYPE.SHAKE)
        {
            if (!wasPrevStationServe)
            {
                lid.transform.localPosition = Vector2.Lerp(lidPos, lidPos + new Vector2(0, 10f), lidTimer / animTime);
            }
            lid.SetActive(true);
        }
        
        // failsafe
        if (GameManager.Instance.currentStation == STATION_TYPE.SERVE)
        {
            lid.transform.localPosition = lidPos;
        }
        
        // disable lid
        if (GameManager.Instance.currentStation < STATION_TYPE.SHAKE)
        {
            lid.transform.localPosition = Vector2.Lerp(lidPos + new Vector2(0, 10f), lidPos, lidTimer / animTime);
            if (lidTimer <= 0)
            {
                lid.SetActive(false);
            }
        }
        
        // enable straw
        if (GameManager.Instance.currentStation == STATION_TYPE.SERVE)
        {
            straw.transform.localPosition = Vector2.Lerp(strawPos, strawPos + new Vector2(0, 10f), strawTimer / animTime);
            straw.SetActive(true);
        }
        
        // disable straw
        if (GameManager.Instance.currentStation < STATION_TYPE.SERVE)
        {
            straw.transform.localPosition = Vector2.Lerp(strawPos + new Vector2(0, 10f), strawPos, strawTimer / animTime);
            if (strawTimer <= 0)
            {
                straw.SetActive(false);
            }
        }

        // Decrement Timers
        if (lidTimer > 0)
        {
            lidTimer -= Time.deltaTime;
        }
        if (strawTimer > 0)
        {
            strawTimer -= Time.deltaTime;
        }
        
        // set prev station
        prevStation = GameManager.Instance.currentStation;
        
        // pls ignore
        if (GameManager.Instance.currentStation == STATION_TYPE.SERVE)
        {
            wasPrevStationServe = true;
        }

        if (GameManager.Instance.currentStation < STATION_TYPE.SHAKE)
        {
            wasPrevStationServe = false;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class TapiocaDispenser : MonoBehaviour
{
    public static TapiocaDispenser Instance;

    [SerializeField] private GameObject tapiocaPrefab;
    [SerializeField] private TapiocaSO[] tapiocaSOList;
    [SerializeField] private List<GameObject> tapiocaList;

    private TAPIOCA_TYPE currentTapiocaType = TAPIOCA_TYPE.NONE;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        tapiocaList = new List<GameObject>();
    }
    public void DispenseTapioca()
    {
        if (currentTapiocaType != TAPIOCA_TYPE.NONE)
        {
            GameObject spawnedTapioca = Instantiate(tapiocaPrefab, transform.position, Quaternion.identity);
            spawnedTapioca.GetComponent<Rigidbody2D>().velocity = Vector2.down * 100f;
            spawnedTapioca.GetComponent<Tapioca>().setTapiocaType(currentTapiocaType);
            tapiocaList.Add(spawnedTapioca);
            OrderManager.Instance.currentOrder.CheckTapiocaType(currentTapiocaType);
        }
    }

    private void SetTapiocaType(TAPIOCA_TYPE type)
    {
        foreach (TapiocaSO tapioca in tapiocaSOList)
        {
            if (tapioca.tapiocaType == type)
            {
                // Set tapioca type and sprite
                currentTapiocaType = tapioca.tapiocaType;
                tapiocaPrefab.GetComponent<SpriteRenderer>().sprite = tapioca.tapiocaSprite;
            }
        }
    }

    public void SetBrownTapioca()
    {
        SetTapiocaType(TAPIOCA_TYPE.BROWN);
    }
    
    public void SetRedTapioca()
    {
        SetTapiocaType(TAPIOCA_TYPE.RED);
    }
    
    public void SetOrangeTapioca()
    {
        SetTapiocaType(TAPIOCA_TYPE.ORANGE);
    }

    public void ResetTapioca()
    {
        foreach(GameObject tapioca in tapiocaList)
        {
            Destroy(tapioca);
        }

        tapiocaList.Clear();    
    }
}

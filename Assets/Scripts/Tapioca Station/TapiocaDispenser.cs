using UnityEngine;

public class TapiocaDispenser : MonoBehaviour
{
    [SerializeField] private GameObject tapiocaPrefab;
    [SerializeField] private TapiocaSO[] tapiocaSOList;

    private TAPIOCA_TYPE currentTapiocaType = TAPIOCA_TYPE.NONE;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetTapiocaType(TAPIOCA_TYPE.TBD);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            SetTapiocaType(TAPIOCA_TYPE.TODO);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SetTapiocaType(TAPIOCA_TYPE.NAMEHERE);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DispenseTapioca();
        }
    }

    private void DispenseTapioca()
    {
        if (currentTapiocaType != TAPIOCA_TYPE.NONE)
        {
            Instantiate(tapiocaPrefab, transform.position, Quaternion.identity);
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
}

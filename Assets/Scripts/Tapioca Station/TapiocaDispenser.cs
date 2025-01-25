using UnityEngine;

public class TapiocaDispenser : MonoBehaviour
{
    [SerializeField] private GameObject tapiocaPrefab;
    [SerializeField] private TapiocaSO[] tapiocaSOList;

    private TAPIOCA_TYPE currentTapiocaType = TAPIOCA_TYPE.NONE;

    public void DispenseTapioca()
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
}

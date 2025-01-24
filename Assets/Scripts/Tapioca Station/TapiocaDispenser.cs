using UnityEngine;

public class TapiocaDispenser : MonoBehaviour
{
    [SerializeField] private GameObject tapiocaPrefab;
    [SerializeField] private TapiocaSO[] tapiocaSO;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DispenseTapioca()
    {
        Instantiate(tapiocaPrefab, transform.position, Quaternion.identity);
    }

    private void SetTapiocaType(TAPIOCA_TYPE type)
    {
        tapiocaPrefab.GetComponent<SpriteRenderer>().sprite = tapiocaSO[0].tapiocaSprite;
    }
}

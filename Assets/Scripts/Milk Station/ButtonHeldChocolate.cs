using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHeldChocolate : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isButtonDown;

    public void OnPointerDown(PointerEventData eventData)
    {
        MilkDispenser.Instance.SetChocolateMilk();
        isButtonDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonDown = false;
    }

}

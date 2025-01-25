using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHeld : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isButtonDown;

    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonDown = true;
        Debug.Log("Down");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonDown = false;
        Debug.Log("up");
    }

}

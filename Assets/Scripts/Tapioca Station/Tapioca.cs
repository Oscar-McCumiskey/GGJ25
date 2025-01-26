using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tapioca : MonoBehaviour
{
    private TAPIOCA_TYPE tapiocaType = TAPIOCA_TYPE.NONE;

    // Update is called once per frame
    public void setTapiocaType(TAPIOCA_TYPE setTapiocaType)
    {
        tapiocaType = setTapiocaType;
    }

    public TAPIOCA_TYPE getTapiocaType()
    {
        return tapiocaType;
    }
}

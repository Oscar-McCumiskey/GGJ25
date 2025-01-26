using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumCup : MonoBehaviour
{
    public static MediumCup Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCup : MonoBehaviour
{
    public static SmallCup Instance;

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

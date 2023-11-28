using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        /*var obj = FindObjectsOfType<DontDestroy>();
        if (obj.Length == 1)*/
        {
            DontDestroyOnLoad(gameObject);
        }
        /*else
        {
            Destroy(gameObject);
        }*/
    }
}
    

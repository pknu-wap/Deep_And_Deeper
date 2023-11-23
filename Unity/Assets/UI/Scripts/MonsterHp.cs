using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHp : MonoBehaviour
{
    public GameObject Monster;
    
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(Monster.transform.position + new Vector3(-2f, 4f));
    }
    
}

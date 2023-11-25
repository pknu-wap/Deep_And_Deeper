using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    public int startingMoney = 50;
    public int currentMoney;

    private void Start()
    {
        currentMoney = startingMoney;
        moneyText.text = currentMoney.ToString();
    }

    public void GetMoney()
    {
        moneyText.text = currentMoney.ToString();
    }
    
}

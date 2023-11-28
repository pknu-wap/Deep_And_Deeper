using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public enum TriggerType
{
    Roll,
    Jump,
    Attack
}

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tutorialText;
    [SerializeField] private TriggerType currenTriggerType;
    private Monster.Monster _monster;

    private void Start()
    {
        DisplayMessage("Press A/D or the left/right arrow keys\nto move.");
        _monster = GameObject.FindWithTag("Monster").GetComponent<Monster.Monster>();
    }

    private void Update()
    {
        if (_monster.isDead)
        {
            DisplayMessage("Tutorial completed!\nPress the upper-right button\nto return to the main screen.");
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player")) return;

        switch (currenTriggerType)
        {
            case TriggerType.Roll:
                DisplayMessage("Press X\nto pass through.");
                break;
            case TriggerType.Jump:
                DisplayMessage("Press the SPACE key\nto jump.");
                break;
            case TriggerType.Attack:
                DisplayMessage("Press Z\nto attack.");
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(!other.CompareTag("Player")) return;
      
        DisplayMessage(null);
    }

    private void DisplayMessage(string message)
    {
        _tutorialText.text = message;
    }
     
}
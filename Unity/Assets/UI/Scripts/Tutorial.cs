using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private string message;

    private Monster.Monster _monster;

    private void Start()
    {
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
        DisplayMessage(message);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        DisplayMessage("");
    }

    private void DisplayMessage(string text)
    {
        textMeshProUGUI.text = text;
    }
}
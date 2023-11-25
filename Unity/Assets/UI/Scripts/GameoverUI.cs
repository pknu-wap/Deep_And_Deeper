using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void OnGameOver()
    {
        gameOverPanel.SetActive(true);
        StartCoroutine(PauseAfterDelay(0.8f));
    }

    private IEnumerator PauseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Time.timeScale = 0f;
    }
}

using System.Collections;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private float pauseDelay = 2f;

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void OnGameOver()
    {
        gameOverPanel.SetActive(true);
        StartCoroutine(PauseAfterDelay(pauseDelay));
    }

    private static IEnumerator PauseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Time.timeScale = 0f;
    }
}
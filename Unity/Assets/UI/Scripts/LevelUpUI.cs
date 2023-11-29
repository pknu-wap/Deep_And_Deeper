using System.Collections;
using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] private GameObject levelUpPanel;
    //[SerializeField] private float pauseDelay = 2f;

    private void Start()
    {
        levelUpPanel.SetActive(false);
    }

    public void OnLevelUp() //레벨업하면 되도록
    {
        levelUpPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    
    //버튼 클릭하면 거가 할당된 특성 얻고, Time.timeScale =1f, levelUpPanel.SetActive(false);
    
}
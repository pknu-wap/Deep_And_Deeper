using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    // [SerializeField] private Image expBar;
    // [SerializeField] private TextMeshProUGUI levelText;
    // public float currentExp = 0f;
    // public float exp = 100f; //한 레벨 당 총 경험치
    // public int level = 1;
    //
    // private void Start()
    // {
    //     PlayerXp();
    //     expBar.fillAmount = currentExp / exp;
    //     levelText.text = level.ToString();
    // }
    //
    // public void PlayerXp()
    // {
    //     exp = level * 100;
    // }
    //
    // public void levelUp()
    // {
    //     StartCoroutine(expUpdate()); 
    //     if (currentExp >= exp)
    //     {
    //         currentExp -= exp;
    //         level++;
    //         levelText.text = level.ToString(); //level 표시 ui
    //         
    //         PlayerXp();
    //         StartCoroutine(expUpdate()); 
    //     }
    // }
    //
    // IEnumerator expUpdate() //나중에 수정
    // {
    //     float elapsedTime = 0f;
    //     float duration = 0.3f; //변경되는데 걸리는 시간
    //
    //     float currentFillAmount = expBar.fillAmount;
    //     float targetFillAmount;
    //
    //     if (currentExp >= exp)
    //     {
    //         targetFillAmount = 1f;
    //     }
    //     else 
    //     {
    //         targetFillAmount = currentExp / exp; 
    //     }
    //
    // while (elapsedTime < duration)
    //     {
    //         expBar.fillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, elapsedTime / duration);
    //         elapsedTime += Time.deltaTime;
    //         yield return null;
    //     }
    //     
    //     expBar.fillAmount = targetFillAmount; //보장을 위해 최종값 설정
    // }
}

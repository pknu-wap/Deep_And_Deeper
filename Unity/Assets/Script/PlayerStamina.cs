using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//스태미나 바 조절 + 스태미나 조절을 위한 기능들 구현
public class PlayerStamina : MonoBehaviour
{
    [SerializeField] private Image staminaBar;
    private const float StartingStamina = 100f;
    private float _currentStamina;
    [SerializeField] private float decreaseAmount = 20f; //감소량
    [SerializeField] private float recoverAmount = 5f; //회복량
    private const float DecreaseDuration = 0.2f;
    private const float IncreaseDuration = 1f;

    private void Awake()
    {
        _currentStamina = StartingStamina;
        staminaBar.fillAmount = _currentStamina / StartingStamina;
    }

    private void Start()
    {
        InvokeRepeating(nameof(RecoverStamina), 0f, 1f);
    }

    public bool CheckRoll()
    {
        return _currentStamina >= 20f;
    }

    public void DeStamina()
    {
        if (CheckRoll())
        {
            StartCoroutine(Decrease());
        }
    }

    private IEnumerator Decrease()
    {
        var elapsedTime = 0f;

        var currentFillAmount = staminaBar.fillAmount;
        var targetFillAmount = (_currentStamina - decreaseAmount) / StartingStamina;

        while (elapsedTime < DecreaseDuration)
        {
            staminaBar.fillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, elapsedTime / DecreaseDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        staminaBar.fillAmount = targetFillAmount; //보장을 위해 최종값 설정
        _currentStamina -= decreaseAmount; // 스태미나 실제 값 감소
    }

    private void RecoverStamina()
    {
        if (_currentStamina < StartingStamina)
        {
            StartCoroutine(Increase());
        }
    }

    private IEnumerator Increase()
    {
        var elapsedTime = 0f;

        var currentFillAmount = staminaBar.fillAmount;
        var targetFillAmount = Mathf.Clamp((_currentStamina + recoverAmount) / StartingStamina, 0f, 1f);

        while (elapsedTime < IncreaseDuration)
        {
            staminaBar.fillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, elapsedTime / IncreaseDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        staminaBar.fillAmount = targetFillAmount; // 보장을 위해 최종값 설정
        _currentStamina += recoverAmount; // 스태미나 실제 값 증가
    }
}
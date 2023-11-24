using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//스태미나 바 조절 + 스태미나 조절을 위한 기능들 구현
public class PlayerStamina : MonoBehaviour
{
    [SerializeField] private Image staminaBar;
    private float startingStamina = 100f;
    private float currentStamina;
    [SerializeField] private float decreasAmount = 20f; //감소량
    [SerializeField] private float recoverAmount = 5f; //회복량

    private void Awake()
    {
        currentStamina = startingStamina;
        staminaBar.fillAmount = currentStamina / startingStamina;
    }

    private void Start()
    {
        InvokeRepeating("RecoverStamina", 0f, 1f);
    }

    public bool CheckRoll()
    {
        return currentStamina >= 20f ? true : false;
    }

    public void DeStamina()
    {
        if (CheckRoll())
        {
            StartCoroutine(Decrease());
        }
    }

    IEnumerator Decrease()
    {
        float elapsedTime = 0f;
        float duration = 0.2f; //변경되는데 걸리는 시간

        float currentFillAmount = staminaBar.fillAmount;
        float targetFillAmount = (currentStamina - decreasAmount) / startingStamina;

        while (elapsedTime < duration)
        {
            staminaBar.fillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        staminaBar.fillAmount = targetFillAmount; //보장을 위해 최종값 설정
        currentStamina -= decreasAmount; // 스태미나 실제 값 감소
    }

    private void RecoverStamina()
    {
        if (currentStamina < startingStamina)
        {
            StartCoroutine(Increase());
        }
    }

    IEnumerator Increase()
    {
        float elapsedTime = 0f;
        float duration = 1f; //1초에 걸쳐 회복

        float currentFillAmount = staminaBar.fillAmount;
        float targetFillAmount = Mathf.Clamp((currentStamina + recoverAmount) / startingStamina, 0f, 1f);

        while (elapsedTime < duration)
        {
            staminaBar.fillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        staminaBar.fillAmount = targetFillAmount; // 보장을 위해 최종값 설정
        currentStamina += recoverAmount; // 스태미나 실제 값 증가
    }
    
    
    
}

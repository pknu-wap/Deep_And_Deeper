using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private TextMeshProUGUI _healthText;
    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        startingHealth = 1000f;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _healthBar.fillAmount = health / startingHealth;
        _healthText.text = health.ToString() + "/" + startingHealth.ToString();
    }

    public override void OnDamage(float damage)
    {
        if (!dead)
        {
            StartCoroutine(FlashColor());
        }
        base.OnDamage(damage);

        StartCoroutine(HpUpdate());
    }
    
    IEnumerator FlashColor()
    {
        Color damagedColor = Color.red;
        Color defaltColor = _spriteRenderer.color;
        
        _spriteRenderer.color = damagedColor;
        
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.color = defaltColor;
    }

    IEnumerator HpUpdate() //나중에 수정
    {
        float elapsedTime = 0f;
        float duration = 0.3f; // 변경되기를 원하는 시간

        float currentFillAmount = _healthBar.fillAmount;
        float targetFillAmount = health / startingHealth;

        while (elapsedTime < duration)
        {
            _healthBar.fillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        _healthText.text = health.ToString() + "/" + startingHealth.ToString();
        _healthBar.fillAmount = targetFillAmount; // 보장을 위해 최종값 설정
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "MeleeAttack")
        {
            OnDamage(50f);
        }
    }
}

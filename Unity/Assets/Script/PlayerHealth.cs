using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private TextMeshProUGUI _healthText;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private bool _isInTrap;
    //public bool isInTrigger; //흠
    private void Awake()
    {
        startingHealth = 1000f; //플레이어가 자진 체력 최대값 -> health가 현재 체력 값
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _isInTrap = false;
        //isInTrigger = false;
    }

    protected override void OnEnable()
    {
        base.OnEnable(); //나중에 수정해야 할 듯, onenable에는 health에 startinghealth를 할당함. 게임매니저에서 해야 하지 안흘까,,?ㅎ

        _healthBar.fillAmount = health / startingHealth;
        _healthText.text = health.ToString() + "/" + startingHealth.ToString();
    }

    /*public override void Die()
    {
        base.Die();
        
    }*/

    public override void OnDamage(float damage)
    {
        if (!dead)
        {
            StartCoroutine(FlashColor());
        }
        //base.OnDamage(damage); //**
        health -= damage;
        StartCoroutine(HpUpdate());
        if (health <= 0 && !dead)
        {
            Die();
        }
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
        float duration = 0.3f; //변경되는데 걸리는 시간

        float currentFillAmount = _healthBar.fillAmount;
        float targetFillAmount = health / startingHealth;

        while (elapsedTime < duration)
        {
            _healthBar.fillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        _healthText.text = health.ToString() + "/" + startingHealth.ToString();
        _healthBar.fillAmount = targetFillAmount; //보장을 위해 최종값 설정
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.CompareTag("MeleeAttack"))
        {
            isInTrigger = true;
        }
        else*/ if (other.CompareTag("Trap"))
        {
            _isInTrap = true;
            StartCoroutine(TrapDamageOverTime());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        /*if (other.CompareTag("MeleeAttack"))
        {
            isInTrigger = false;
        }
        else*/ if (other.CompareTag("Trap"))
        {
            _isInTrap = false;
            StopCoroutine(TrapDamageOverTime());
        }
    }

    IEnumerator TrapDamageOverTime()
    {
        float damageCoolTime = 1f; 
        
        while (_isInTrap)
        {
            OnDamage(10f);
            yield return new WaitForSeconds(damageCoolTime);
        }
    }
}

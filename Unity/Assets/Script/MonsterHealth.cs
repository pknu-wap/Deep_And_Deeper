using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHealth : LivingEntity
{
    [SerializeField] private GameObject monsterHp; //프리팹
    [SerializeField] private GameObject canvas;
    private Image _healthBar;
    private SpriteRenderer _spriteRenderer;
    private RectTransform hpBar;
    public bool isInTrigger;

    private PlayerExp _playerExp;
    
    [SerializeField] private float height = 1.7f; //MonsterHpBar가 머리 우에 오도록

    private void Awake()
    {
        GameObject monsterHpBar = Instantiate(monsterHp, canvas.transform);
        hpBar = monsterHpBar.GetComponent<RectTransform>();
        startingHealth = 200f; //몬스터가 가진 체력 최대값 -> health가 현재 체력 값
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _healthBar = monsterHpBar.transform.GetChild(0).GetComponent<Image>();
        isInTrigger = false;
        _playerExp = GameObject.FindWithTag("Player").GetComponent<PlayerExp>();
    }
    
    private void Update()
    {
        Vector3 _hpBarPos =
            Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0f));
        hpBar.position = _hpBarPos;
    }
    
    protected override void OnEnable()
    {
        base.OnEnable(); 

        _healthBar.fillAmount = health / startingHealth;
    }
    public override void Die()
    {
        base.Die();
        gameObject.SetActive(false); //나중에 수정

        _playerExp.currentExp += 10f;
        _playerExp.levelUp();
    }
    
    public override void OnDamage(float damage)
    {
        if (!dead)
        {
            StartCoroutine(FlashColor());
        }
        base.OnDamage(damage);
        
        float targetFillAmount = health / startingHealth;
        _healthBar.fillAmount = targetFillAmount;
    }
    
    IEnumerator FlashColor()
    {
        Color damagedColor = Color.red;
        Color defaltColor = _spriteRenderer.color;
        
        _spriteRenderer.color = damagedColor;
        
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.color = defaltColor;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttack")) isInTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttack")) isInTrigger = false;
    }

}

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
    
    [SerializeField] private float height = 1.7f; //MonsterHpBar가 머리 우에 오도록

    private void Awake()
    {
        GameObject monsterHpBar = Instantiate(monsterHp, canvas.transform);
        hpBar = monsterHpBar.GetComponent<RectTransform>();
        startingHealth = 200f; //몬스터가 가진 체력 최대값 -> health가 현재 체력 값
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _healthBar = monsterHpBar.transform.GetChild(0).GetComponent<Image>();
        isInTrigger = false;
    }
    
    /*private void Start()
    {
        hpBar = Instantiate(monsterHp, canvas.transform).GetComponent<RectTransform>();
    }*/
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

    IEnumerator HpUpdate() //나중에 수정. 굳이 서서히 감소할 필요가 있을까..?
    {
        float elapsedTime = 0f;
        float duration = 0.2f; //변경되는데 걸리는 시간

        float currentFillAmount = _healthBar.fillAmount;
        float targetFillAmount = health / startingHealth;

        while (elapsedTime < duration)
        {
            _healthBar.fillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        _healthBar.fillAmount = targetFillAmount; //보장을 위해 최종값 설정
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

public class PlayerHealth : LivingEntity
{
//     [SerializeField] private Image healthBar;
//     [SerializeField] private TextMeshProUGUI healthText;
//     private SpriteRenderer _spriteRenderer;
//
//     private bool _isInTrap;
//
//     private const float HpUpdateDuration = 0.3f;
//     private const float TrapDamageCooldown = 1.0f;
//
//     private void Awake()
//     {
//         startingHealth = 1000f; //플레이어가 자진 체력 최대값 -> health가 현재 체력 값
//         _spriteRenderer = GetComponent<SpriteRenderer>();
//         _isInTrap = false;
//         //isInTrigger = false;
//     }
//
//     protected override void OnEnable()
//     {
//         base.OnEnable(); //나중에 수정해야 할 듯, OnEnable에는 health에 StartingHealth를 할당함. 게임매니저에서 해야 하지 안흘까,,?ㅎ
//
//         healthBar.fillAmount = health / startingHealth;
//         healthText.text = health + "/" + startingHealth;
//     }
//
//     /*public override void Die()
//     {
//         base.Die();
//
//     }*/
//
//     public override void OnDamage(float damage)
//     {
//         if (!dead)
//         {
//             StartCoroutine(FlashColor());
//         }
//
//         //base.OnDamage(damage); //**
//         health -= damage;
//         StartCoroutine(HpUpdate());
//         if (health <= 0 && !dead)
//         {
//             Die();
//         }
//     }
//
//     private IEnumerator FlashColor()
//     {
//         var damagedColor = Color.red;
//         var defaultColor = _spriteRenderer.color;
//
//         _spriteRenderer.color = damagedColor;
//
//         yield return new WaitForSeconds(0.2f);
//
//         // ReSharper disable once Unity.InefficientPropertyAccess
//         _spriteRenderer.color = defaultColor;
//     }
//
//     private IEnumerator HpUpdate() //나중에 수정
//     {
//         var elapsedTime = 0f;
//
//         var currentFillAmount = healthBar.fillAmount;
//         var targetFillAmount = health / startingHealth;
//
//         while (elapsedTime < HpUpdateDuration)
//         {
//             healthBar.fillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, elapsedTime / HpUpdateDuration);
//             elapsedTime += Time.deltaTime;
//             yield return null;
//         }
//
//         healthText.text = health + "/" + startingHealth;
//         healthBar.fillAmount = targetFillAmount; //보장을 위해 최종값 설정
//     }
//
//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         /*if (other.CompareTag("MeleeAttack"))
//         {
//             isInTrigger = true;
//         }
//         else*/
//
//         if (!other.CompareTag("Trap")) return;
//
//         _isInTrap = true;
//         StartCoroutine(TrapDamageOverTime());
//     }
//
//     private void OnTriggerExit2D(Collider2D other)
//     {
//         /*if (other.CompareTag("MeleeAttack"))
//         {
//             isInTrigger = false;
//         }
//         else*/
//
//         if (!other.CompareTag("Trap")) return;
//
//         _isInTrap = false;
//         StopCoroutine(TrapDamageOverTime());
//     }
//
//     private IEnumerator TrapDamageOverTime()
//     {
//         while (_isInTrap)
//         {
//             OnDamage(10f);
//             yield return new WaitForSeconds(TrapDamageCooldown);
//         }
//     }
}
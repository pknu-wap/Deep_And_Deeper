using UnityEngine;
using UnityEngine.UI;

namespace Monster
{
    public class MonsterHealth : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private Color hitColor = Color.red;
        [SerializeField] private Color originColor = Color.white;
        [SerializeField] private float hitEffectDuration = 0.2f;

        private SpriteRenderer _spriteRenderer;
        private Animator _animator;

        private float _health;
        private Image _healthBar;
        private float _hitTimer;

        private void Awake()
        {
            _healthBar = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        }

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        private void OnDamaged(float damage)
        {
            _health -= damage;
            UpdateHealthUI();

            _hitTimer = hitEffectDuration;
            SetColor(hitColor);

            if (_health > 0) return;

            _animator.Play("Die");
        }

        private void UpdateHealthUI()
        {
            _healthBar.fillAmount = _health / maxHealth;
        }

        private void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }

        private void Update()
        {
            HandleHitEffect();

            if (Input.GetKeyDown(KeyCode.Q))
            {
                OnDamaged(10);
            }
        }

        private void HandleHitEffect()
        {
            if (_hitTimer == 0) return;

            _hitTimer -= Time.deltaTime;

            if (_hitTimer > 0) return;

            _hitTimer = 0;
            SetColor(originColor);
        }
    }
}
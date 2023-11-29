using Hero;
using UnityEngine;
using UnityEngine.UI;

namespace Monster
{
    public class Monster : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        [SerializeField] public float maxHealth;
        [SerializeField] private Color hitColor = Color.red;
        [SerializeField] private Color originColor = Color.white;
        [SerializeField] private float hitEffectDuration = 0.2f;
        [SerializeField] private Slider slider;
        [SerializeField] private float phase2Ratio;

        public bool phase2;
        public bool isDead;

        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;

        private Vector3 _originScale;
        private Vector3 _flippedScale;
        public float _health;
        private float _hitTimer;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();

            _originScale = transform.localScale;
            _flippedScale = _originScale;
            _flippedScale.x *= -1;

            _health = maxHealth;
            UpdateHealthUI();
        }

        private void Update()
        {
            HandleHitEffect();

            if (Input.GetKeyDown(KeyCode.Q))
            {
                OnDamaged(10);
            }
        }

        private void UpdateHealthUI()
        {
            slider.value = _health / maxHealth;
        }

        private void HandleHitEffect()
        {
            if (_hitTimer == 0) return;

            _hitTimer -= Time.deltaTime;

            if (_hitTimer > 0) return;

            _hitTimer = 0;
            SetColor(originColor);
        }

        private void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }

        public void OnDamaged(float damage)
        {
            _health -= damage;
            UpdateHealthUI();

            _hitTimer = hitEffectDuration;
            SetColor(hitColor);

            if (_health <= maxHealth * phase2Ratio)
            {
                phase2 = true;
            }

            if (_health > 0) return;

            _animator.Play("Death");
            isDead = true;
            SetHealthUIActive(false);
        }

        public void SetState(string stateName)
        {
            if (isDead) return;

            _animator.Play(stateName);
        }

        public void ChaseDirection()
        {
            if (isDead) return;

            var flipped = transform.position.x < HeroManager.Instance.GetPosition().x;
            transform.localScale = flipped ? _flippedScale : _originScale;
        }

        public void Chase()
        {
            if (isDead) return;

            var flipped = transform.position.x < HeroManager.Instance.GetPosition().x;
            transform.localScale = flipped ? _flippedScale : _originScale;

            var x = flipped ? 1 : -1;
            _rigidbody2D.velocity = new Vector2(x * speed, _rigidbody2D.velocity.y);
        }

        private void SetHealthUIActive(bool visible)
        {
            slider.gameObject.SetActive(visible);
        }

        public void Chase(float customSpeed)
        {
            if (isDead) return;

            var flipped = transform.position.x < HeroManager.Instance.GetPosition().x;
            transform.localScale = flipped ? _flippedScale : _originScale;

            var x = flipped ? 1 : -1;
            _rigidbody2D.velocity = new Vector2(x * customSpeed, _rigidbody2D.velocity.y);
        }

        public void MultiplyScale(float scale)
        {
            transform.localScale *= scale;
            _originScale *= scale;
            _flippedScale *= scale;
        }
    }
}
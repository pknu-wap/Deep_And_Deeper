using System.Globalization;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace Hero
{
    public class HeroManager
    {
        private static HeroManager _instance;

        private readonly float _maxHealth;
        private readonly float _maxStamina;
        private readonly float _hitEffectDuration;
        private readonly float _staminaRecoverAmount;

        private float _health;
        public float Stamina;
        public float Money;
        public bool IsDead;

        private Image _healthBar;
        private Image _staminaBar;
        private TextMeshProUGUI _healthText;
        private TextMeshProUGUI _moneyText;

        private readonly Color _hitColor = Color.red;
        private readonly Color _originColor = Color.white;

        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private Transform _transform;

        private GameOverUI _gameOverUI;

        private bool _isGrounded;
        private float _hitTimer;

        public static HeroManager Instance => _instance ??= new HeroManager();

        private void Init()
        {
            new GameObject().AddComponent<UpdateShuttle>();

            var playerObject = GameObject.FindWithTag("Player");

            // if (gameObject != null)
            {
                _rigidbody2D = playerObject.GetComponent<Rigidbody2D>();
                _animator = playerObject.GetComponent<Animator>();
                _spriteRenderer = playerObject.GetComponent<SpriteRenderer>();
                _transform = playerObject.GetComponent<Transform>();
                _gameOverUI = playerObject.GetComponent<GameOverUI>();
            }

            _healthBar = GameObject.FindWithTag("HealthBar").GetComponent<Image>();
            _healthText = GameObject.FindWithTag("HealthText").GetComponent<TextMeshProUGUI>();
            UpdateHealthUI();

            _staminaBar = GameObject.FindWithTag("StaminaBar").GetComponent<Image>();
            UpdateStaminaUI();

            _moneyText = GameObject.FindWithTag("MoneyText").GetComponent<TextMeshProUGUI>();
            UpdateMoneyUI();
        }

        private HeroManager()
        {
            var heroManagerDataContainer = Resources.Load<GameObject>("HeroManagerDataContainer");
            var heroManagerData = heroManagerDataContainer.GetComponent<HeroManagerData>();

            _maxHealth = heroManagerData.maxHealth;
            _health = _maxHealth;
            _maxStamina = heroManagerData.maxStamina;
            Stamina = _maxStamina;
            Money = heroManagerData.initialMoney;
            _hitEffectDuration = heroManagerData.hitEffectDuration;
            _staminaRecoverAmount = heroManagerData.staminaRecoverAmount;

            Init();
        }

        public void SetVelocityX(float x)
        {
            _rigidbody2D.velocity = new Vector2(x, _rigidbody2D.velocity.y);
        }

        public void SetVelocityY(float y)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, y);
        }

        public void SetState(string stateName)
        {
            _animator.Play(stateName);
        }

        public void SetFlipX(bool flipX)
        {
            _spriteRenderer.flipX = flipX;
        }

        public bool GetFlipX()
        {
            return _spriteRenderer.flipX;
        }

        public void SetGrounded(bool grounded)
        {
            _isGrounded = grounded;
        }

        public bool GetGrounded()
        {
            return _isGrounded;
        }

        public Vector3 GetPosition()
        {
            return _transform.position;
        }

        public void OnDamaged(float damage)
        {
            _health -= damage;
            UpdateHealthUI();

            _hitTimer = _hitEffectDuration;
            SetColor(_hitColor);

            if (_health > 0) return;

            IsDead = true;
            SetState("Die");
            GameOver();
        }

        private void UpdateHealthUI()
        {
            _healthBar.fillAmount = _health / _maxHealth;
            _healthText.text = _health + "/" + _maxHealth;
        }

        public void ConsumeStamina(float cost)
        {
            Stamina -= cost;
            UpdateStaminaUI();
        }

        private void UpdateStaminaUI()
        {
            _staminaBar.fillAmount = Stamina / _maxStamina;
        }

        private void GameOver()
        {
            _gameOverUI.OnGameOver();
        }

        private void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }

        private void HandleHitEffect()
        {
            if (_hitTimer == 0) return;

            _hitTimer -= Time.deltaTime;

            if (_hitTimer > 0) return;

            _hitTimer = 0;
            SetColor(_originColor);
        }

        private void RecoverStamina()
        {
            Stamina = math.min(_maxStamina, Stamina + _staminaRecoverAmount);
            UpdateStaminaUI();
        }

        public void Update()
        {
            HandleHitEffect();
            RecoverStamina();
        }

        private void UpdateMoneyUI()
        {
            _moneyText.text = Money.ToString(CultureInfo.InvariantCulture);
        }
    }
}
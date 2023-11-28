using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        public float Health;
        public float Stamina;
        public int Money;
        public int Level;
        public int Exp;
        private int _maxExp;
        public bool IsDead;

        private Image _healthBar;
        private Image _staminaBar;
        private Image _expBar;
        private TextMeshProUGUI _healthText;
        private TextMeshProUGUI _moneyText;
        private TextMeshProUGUI _levelText;

        private readonly Color _hitColor = Color.red;
        private readonly Color _originColor = Color.white;

        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private Transform _transform;
        private CapsuleCollider2D[] _capsuleCollider2D;

        private GameOverUI _gameOverUI;

        private bool _isGrounded;
        private float _hitTimer;

        public static HeroManager Instance => _instance ??= new HeroManager();

        private void Init()
        {
            new GameObject("Update Shuttle").AddComponent<UpdateShuttle>();

            var playerObject = GameObject.FindWithTag("Player");

            if (playerObject != null)
            {
                _rigidbody2D = playerObject.GetComponent<Rigidbody2D>();
                _animator = playerObject.GetComponent<Animator>();
                _spriteRenderer = playerObject.GetComponent<SpriteRenderer>();
                _transform = playerObject.GetComponent<Transform>();
                _gameOverUI = playerObject.GetComponent<GameOverUI>();
                _capsuleCollider2D = playerObject.GetComponents<CapsuleCollider2D>();
            }

            _healthBar = GameObject.FindWithTag("HealthBar").GetComponent<Image>();
            _healthText = GameObject.FindWithTag("HealthText").GetComponent<TextMeshProUGUI>();
            UpdateHealthUI();

            _staminaBar = GameObject.FindWithTag("StaminaBar").GetComponent<Image>();
            UpdateStaminaUI();

            _moneyText = GameObject.FindWithTag("MoneyText").GetComponent<TextMeshProUGUI>();
            UpdateMoneyUI();

            Level = 1;
            _maxExp = GetMaxExp(Level);

            _levelText = GameObject.FindWithTag("LevelText").GetComponent<TextMeshProUGUI>();
            _expBar = GameObject.FindWithTag("ExpBar").GetComponent<Image>();
            UpdateLevelUI();
            UpdateExpUI();
        }

        private HeroManager()
        {
            var heroManagerDataContainer = Resources.Load<GameObject>("HeroManagerDataContainer");
            var heroManagerData = heroManagerDataContainer.GetComponent<HeroManagerData>();

            _maxHealth = heroManagerData.maxHealth;
            Health = _maxHealth;
            _maxStamina = heroManagerData.maxStamina;
            Stamina = _maxStamina;
            Money = heroManagerData.initialMoney;
            _hitEffectDuration = heroManagerData.hitEffectDuration;
            _staminaRecoverAmount = heroManagerData.staminaRecoverAmount;

            Init();
        }

        public void SetVelocityX(float x)
        {
            if (IsDead) return;

            _rigidbody2D.velocity = new Vector2(x, _rigidbody2D.velocity.y);
        }

        public void SetVelocityY(float y)
        {
            if (IsDead) return;

            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, y);
        }

        public void SetState(string stateName)
        {
            if (IsDead) return;

            _animator.Play(stateName);
        }

        public void SetFlipX(bool flipX)
        {
            if (IsDead) return;

            _spriteRenderer.flipX = flipX;
        }

        public bool GetFlipX()
        {
            return _spriteRenderer.flipX;
        }

        public void SetGrounded(bool grounded)
        {
            if (IsDead) return;

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
            Health -= damage;
            UpdateHealthUI();

            _hitTimer = _hitEffectDuration;
            SetColor(_hitColor);

            if (Health > 0) return;

            SetState("Death");
            IsDead = true;
            GameOver();
        }

        private void UpdateHealthUI()
        {
            _healthBar.fillAmount = Health / _maxHealth;
            _healthText.text = Health + "/" + _maxHealth;
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

            if (Input.GetKeyDown(KeyCode.E)) AddExp(10);
        }

        public void Update()
        {
            HandleHitEffect();
            RecoverStamina();

            if (Input.GetKeyDown(KeyCode.N))
            {
                SceneManager.LoadScene("BattleMap1_2");
            }
        }

        private void UpdateMoneyUI()
        {
            _moneyText.text = Money.ToString();
        }

        private static int GetMaxExp(int level)
        {
            return 70 + level * 30;
        }

        private void AddExp(int exp)
        {
            Exp += exp;

            while (Exp >= _maxExp)
            {
                Exp -= _maxExp;
                Level++;
                _maxExp = GetMaxExp(Level);
                UpdateLevelUI();
                UpdateExpUI();
            }
        }

        private void UpdateLevelUI()
        {
            _levelText.text = Level.ToString();
        }

        private void UpdateExpUI()
        {
            _expBar.fillAmount = 1f * Exp / _maxExp;
        }

        public void RollAndResize()
        {
            _capsuleCollider2D[0].enabled = false;
            _capsuleCollider2D[1].enabled = true;
        }

        public void ResetColliderSize()
        {
            _capsuleCollider2D[0].enabled = true;
            _capsuleCollider2D[1].enabled = false;
        }
    }
}
using Audio;
using MBT;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Hero
{
    public class HeroManager : MonoBehaviour
    {
        private static HeroManager _instance;

        public static HeroManager Instance
        {
            get
            {
                if (_instance) return _instance;

                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                _instance = new GameObject("Hero Manager").AddComponent<HeroManager>();

                DontDestroyOnLoad(_instance.gameObject);

                return _instance;
            }
        }

        private void Awake()
        {
            var heroManagerDataContainer = Resources.Load<GameObject>("HeroManagerDataContainer");
            var heroManagerData = heroManagerDataContainer.GetComponent<HeroManagerData>();

            _maxHealth = heroManagerData.maxHealth;
            health = _maxHealth;
            _maxStamina = heroManagerData.maxStamina;
            stamina = _maxStamina;
            money = heroManagerData.initialMoney;
            _hitEffectDuration = heroManagerData.hitEffectDuration;
            _staminaRecoverAmount = heroManagerData.staminaRecoverAmount;
            moveSpeed = heroManagerData.moveSpeed;

            level = 1;
            _maxExp = GetMaxExp(level);

            OnSceneLoaded();

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded()
        {
            var playerObject = GameObject.FindWithTag("Player");
            var topViewPlayerObject = GameObject.FindWithTag("TopViewHero");

            if (playerObject == null && topViewPlayerObject == null)
            {
                _noHero = true;
                return;
            }

            _noHero = false;

            if (topViewPlayerObject != null)
            {
                topViewTransform = topViewPlayerObject.transform;
                if (MapGenerator.MapGenerator.Instance.needRefresh)
                {
                    SetStage(_stage + 1);
                    MapGenerator.MapGenerator.Instance.CreateMap();
                }
            }

            if (playerObject != null)
            {
                _rigidbody2D = playerObject.GetComponent<Rigidbody2D>();
                _animator = playerObject.GetComponent<Animator>();
                _spriteRenderer = playerObject.GetComponent<SpriteRenderer>();
                _transform = playerObject.GetComponent<Transform>();
                _gameOverUI = playerObject.GetComponent<GameOverUI>();
                // _levelUpUI = playerObject.GetComponent<LevelUpUI>();

                _capsuleCollider2D = GameObject.FindWithTag("HeroCollider").GetComponents<CapsuleCollider2D>();

                _originScale = _transform.localScale;
                _flippedScale = _originScale;
                _flippedScale.x *= -1;
            }

            _healthBar = GameObject.FindWithTag("HealthBar").GetComponent<Image>();
            _healthText = GameObject.FindWithTag("HealthText").GetComponent<TextMeshProUGUI>();
            UpdateHealthUI();

            _staminaBar = GameObject.FindWithTag("StaminaBar").GetComponent<Image>();
            UpdateStaminaUI();

            _moneyText = GameObject.FindWithTag("MoneyText").GetComponent<TextMeshProUGUI>();
            UpdateMoneyUI();

            _levelText = GameObject.FindWithTag("LevelText").GetComponent<TextMeshProUGUI>();
            _expBar = GameObject.FindWithTag("ExpBar").GetComponent<Image>();
            UpdateLevelUI();
            UpdateExpUI();
        }

        private int _stage;
        private bool _isBoss;

        private void OnSceneLoaded(Scene a, LoadSceneMode b)
        {
            OnSceneLoaded();
        }

        private bool _noHero;

        private float _maxHealth;
        private float _maxStamina;
        private float _hitEffectDuration;
        private float _staminaRecoverAmount;
        private bool _isInvincible;
        private float _invincibleTimer;
        private const float InvincibleTime = 0.16f;

        public float moveSpeed;
        public float health;
        public float stamina;
        public int money;
        public int level;
        public int exp;
        private int _maxExp;
        public bool isDead;
        public bool lookingRight = true;
        public Transform topViewTransform;

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
        // private LevelUpUI _levelUpUI;

        public bool isGrounded;
        private float _hitTimer;

        private Vector3 _originScale;
        private Vector3 _flippedScale;

        public void SetVelocityX(float x)
        {
            if (isDead) return;

            _rigidbody2D.velocity = new Vector2(x, _rigidbody2D.velocity.y);
        }

        public void SetVelocityY(float y)
        {
            if (isDead) return;

            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, y);
        }

        public void SetState(string stateName)
        {
            if (isDead) return;

            _animator.Play(stateName);
        }

        public Vector3 GetPosition()
        {
            return _transform.position;
        }

        public void OnDamaged(float damage)
        {
            if (_isInvincible) return;

            health -= damage;
            UpdateHealthUI();

            _hitTimer = _hitEffectDuration;
            SetColor(_hitColor);
            SoundManager.Instance.PlaySfx(Sound.PlayerHurt);

            if (health > 0) return;

            SetState("Death");
            isDead = true;
            SoundManager.Instance.PlaySfx(Sound.PlayerDead);
            GameOver();
        }

        private void UpdateHealthUI()
        {
            _healthBar.fillAmount = health / _maxHealth;
            _healthText.text = health + "/" + _maxHealth;
        }

        public void ConsumeStamina(float cost)
        {
            stamina -= cost;
            UpdateStaminaUI();
        }

        private void UpdateStaminaUI()
        {
            _staminaBar.fillAmount = stamina / _maxStamina;
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
            stamina = math.min(_maxStamina, stamina + _staminaRecoverAmount);
            UpdateStaminaUI();

            if (Input.GetKeyDown(KeyCode.E)) AddExp(10);
        }

        private void SetInvincible()
        {
            _isInvincible = true;
            _invincibleTimer = InvincibleTime;
        }

        private void HandleInvincible()
        {
            if (!_isInvincible) return;

            _invincibleTimer -= Time.deltaTime;

            if (_invincibleTimer > 0) return;

            _isInvincible = false;
        }

        public void Update()
        {
            if (_noHero) return;

            HandleInvincible();
            HandleHitEffect();
            RecoverStamina();

            if (Input.GetKeyDown(KeyCode.N))
            {
                SceneManager.LoadScene("BattleMap1_2");
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                MapGenerator.MapGenerator.Instance.CreateMap();
            }
        }

        private void UpdateMoneyUI()
        {
            _moneyText.text = money.ToString();
        }

        private static int GetMaxExp(int level)
        {
            return 70 + level * 30;
        }

        public void AddExp(int newExp)
        {
            exp += newExp;

            while (exp >= _maxExp)
            {
                exp -= _maxExp;
                level++;
                _maxExp = GetMaxExp(level);
                UpdateLevelUI();
                UpdateExpUI();
                // _levelUpUI.OnLevelUp();
            }
        }

        private void UpdateLevelUI()
        {
            _levelText.text = level.ToString();
        }

        private void UpdateExpUI()
        {
            _expBar.fillAmount = 1f * exp / _maxExp;
        }

        public void RollAndResize()
        {
            SetInvincible();
            _capsuleCollider2D[0].enabled = false;
            _capsuleCollider2D[1].enabled = true;
        }

        public void ResetColliderSize()
        {
            _capsuleCollider2D[0].enabled = true;
            _capsuleCollider2D[1].enabled = false;
        }

        public void SetHealth(float newHealth)
        {
            health = newHealth;
            UpdateHealthUI();
        }

        public void AddHealth(float newHealth)
        {
            health += newHealth;
            UpdateHealthUI();
        }

        public void SetMoney(int newMoney)
        {
            money = newMoney;
            UpdateMoneyUI();
        }

        public void AddMoney(int newMoney)
        {
            money += newMoney;
            UpdateMoneyUI();
        }

        public void SetExp(int newExp)
        {
            exp = newExp;
            AddExp(0);
        }

        public void AddMaxStamina(int newStamina)
        {
            _maxStamina += newStamina;
        }

        public NodeResult Run()
        {
            var inputX = Input.GetAxis("Horizontal");
            if (inputX == 0) return NodeResult.failure;

            var flipped = inputX < 0;
            _transform.localScale = flipped ? _flippedScale : _originScale;
            lookingRight = !flipped;
            SetVelocityX(inputX * moveSpeed);

            return NodeResult.success;
        }

        private void SetStage(int stage)
        {
            _stage = stage;

            SoundManager.Instance.StopAll();
            SoundManager.Instance.PlaySfx(GetSound(_stage, _isBoss));
        }

        private static Sound GetSound(int stage, bool isBoss)
        {
            return stage switch
            {
                1 when !isBoss => Sound.Stage1,
                1 => Sound.Boss1,
                2 when !isBoss => Sound.Stage1,
                2 => Sound.Boss2,
                3 when !isBoss => Sound.Stage1,
                3 => Sound.Boss3,
                _ => Sound.PlayerDead
            };
        }
    }
}
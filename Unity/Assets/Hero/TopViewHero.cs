using UnityEngine;

namespace Hero
{
    public class TopViewHero : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;

        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody2D;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Awake()
        {
            transform.position = MapGenerator.MapGenerator.Instance.savedPosition;
        }

        private void Update()
        {
            MapGenerator.MapGenerator.Instance.savedPosition = transform.position;

            var hInput = Input.GetAxis("Horizontal");
            var vInput = Input.GetAxis("Vertical");

            if (hInput == 0 && vInput == 0)
            {
                _animator.Play("Idle");
                _rigidbody2D.velocity = Vector2.zero;
                return;
            }

            if (hInput != 0) _spriteRenderer.flipX = (hInput < 0);

            _animator.Play("Run");
            var hDirection = Vector2.right * hInput;
            var vDirection = Vector2.up * vInput;
            var direction = hDirection + vDirection;
            direction.Normalize();
            _rigidbody2D.velocity = direction * moveSpeed;
        }
    }
}
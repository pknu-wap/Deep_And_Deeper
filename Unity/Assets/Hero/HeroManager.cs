using UnityEngine;

namespace Hero
{
    public class HeroManager
    {
        private static HeroManager _instance;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly Animator _animator;
        private readonly SpriteRenderer _spriteRenderer;
        private readonly Transform _transform;

        private bool _isGrounded;

        public static HeroManager Instance => _instance ??= new HeroManager();

        private HeroManager()
        {
            var gameObject = GameObject.FindWithTag("Player");
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            _animator = gameObject.GetComponent<Animator>();
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            _transform = gameObject.GetComponent<Transform>();
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
    }
}
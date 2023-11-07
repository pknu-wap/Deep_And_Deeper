using UnityEngine;

namespace Hero
{
    public class HeroManager
    {
        private static HeroManager _instance;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly Animator _animator;
        private readonly SpriteRenderer _spriteRenderer;

        public static HeroManager Instance => _instance ??= new HeroManager();

        private HeroManager()
        {
            var gameObject = GameObject.FindWithTag("Player");
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            _animator = gameObject.GetComponent<Animator>();
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        public void SetVelocity(float? x, float? y)
        {
            var velocity = _rigidbody2D.velocity;
            x ??= velocity.x;
            y ??= velocity.y;
            _rigidbody2D.velocity = new Vector2(x.Value, y.Value);
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
    }
}
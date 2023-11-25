using System;
using UnityEngine;

namespace Hero
{
    public class TopViewHero : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 0.05f;

        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private Transform _transform;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _transform = transform;
        }

        private void Update()
        {
            var hInput = Input.GetAxis("Horizontal");
            var vInput = Input.GetAxis("Vertical");

            if (hInput == 0 && vInput == 0)
            {
                _animator.Play("Idle");
                return;
            }

            if (hInput != 0) _spriteRenderer.flipX = (hInput < 0);

            _animator.Play("Run");
            var hDirection = _transform.right * hInput;
            var vDirection = _transform.up * vInput;
            var direction = hDirection + vDirection;
            direction.Normalize();
            transform.Translate(direction * moveSpeed);
        }
    }
}
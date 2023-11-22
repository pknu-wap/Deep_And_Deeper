using System;
using MBT;
using UnityEngine;

namespace BT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/Roll")]
    public class Roll : Leaf
    {
        [SerializeField] private float moveSpeed = 7f;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody2D;
        private BoxCollider2D _boxCollider2D;
        
        public Vector2 originalSize;
        public Vector2 originalOffset;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
            
            originalSize = _boxCollider2D.size;
            originalOffset = _boxCollider2D.offset;
            
        }

        public override NodeResult Execute()
        {
            var inputX = Input.GetAxis("Horizontal");
                switch (_spriteRenderer.flipX)
                {
                    case true:
                        _rigidbody2D.velocity = new Vector2(-moveSpeed, _rigidbody2D.velocity.y);
                        break;
                    case false:
                        _rigidbody2D.velocity = new Vector2(moveSpeed, _rigidbody2D.velocity.y);
                        break;
                }
            
            _animator.Play("Roll");
            _boxCollider2D.size = new Vector2(_boxCollider2D.size.x, 0.6f);
            _boxCollider2D.offset = new Vector2(_boxCollider2D.offset.x, 0.4f);
            
            return NodeResult.success;
        }
    }
}
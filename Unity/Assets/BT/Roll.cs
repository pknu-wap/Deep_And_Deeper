using MBT;
using UnityEngine;

namespace BT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/Roll")]
    public class Roll : Leaf
    {
        [SerializeField] private float moveSpeed = 7f;
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public override NodeResult Execute()
        {
            var dir = _spriteRenderer.flipX ? -1 : 1;
            _rigidbody2D.velocity = new Vector2(moveSpeed * dir, _rigidbody2D.velocity.y);

            return NodeResult.success;
        }
    }
}
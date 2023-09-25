using MBT;
using UnityEngine;

namespace BT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/Run")]
    public class Run : Leaf
    {
        [SerializeField] private float moveSpeed;
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public override NodeResult Execute()
        {
            var inputX = Input.GetAxis("Horizontal");

            _rigidbody2D.velocity = new Vector2(inputX * moveSpeed, _rigidbody2D.velocity.y);

            _spriteRenderer.flipX = inputX switch
            {
                > 0 => false,
                < 0 => true,
                _ => _spriteRenderer.flipX
            };

            return NodeResult.success;
        }
    }
}
using MBT;
using UnityEngine;

namespace BT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/Run")]
    public class Run : Leaf
    {
        [SerializeField] private float runSpeed = 5f;
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
            if (inputX == 0) return NodeResult.failure;
            
            _spriteRenderer.flipX = inputX < 0;
            _rigidbody2D.velocity = new Vector2(inputX * runSpeed, _rigidbody2D.velocity.y);
            
            return NodeResult.success;
        }
    }
}
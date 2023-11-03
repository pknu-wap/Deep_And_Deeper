using MBT;
using UnityEngine;

namespace BT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/MoveX")]
    public class MoveX : Leaf
    {
        [SerializeField] private float moveSpeed;
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public override NodeResult Execute()
        {
            var inputX = Input.GetAxis("Horizontal");

            
            _rigidbody2D.velocity = new Vector2(inputX * moveSpeed, _rigidbody2D.velocity.y);
            

            if (inputX == 0) return NodeResult.failure;
            return NodeResult.success;
        }
    }
}
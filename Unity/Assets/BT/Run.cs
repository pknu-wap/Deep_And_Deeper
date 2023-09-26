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
        private Animator _animator;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        public override NodeResult Execute()
        {
            var inputX = Input.GetAxis("Horizontal");

            _rigidbody2D.velocity = new Vector2(inputX * moveSpeed, _rigidbody2D.velocity.y);

            /*switch (inputX)
            {
                case > 0:
                    _animator.Play("Run");
                    _spriteRenderer.flipX = false;
                    return NodeResult.success;
                case < 0:
                    _animator.Play("Run");
                    _spriteRenderer.flipX = true;
                    return NodeResult.success;
                default:
                    return NodeResult.failure;
            }*/
            
            if(inputX == 0) return NodeResult.failure;
            
            _animator.Play("Run");
            _spriteRenderer.flipX = inputX < 0;
            return NodeResult.success;
        }
    }
}
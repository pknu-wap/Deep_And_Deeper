using MBT;
using UnityEngine;

namespace BT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/Run")]
    public class Run : Leaf
    {
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        public override NodeResult Execute()
        {
            var inputX = Input.GetAxis("Horizontal");

            if (inputX == 0) return NodeResult.failure;

            _animator.Play("Run");
            _spriteRenderer.flipX = inputX < 0;
            return NodeResult.success;
        }
    }
}
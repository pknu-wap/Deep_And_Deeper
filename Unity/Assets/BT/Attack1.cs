using MBT;
using UnityEngine;

namespace BT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/Attack1")]
    public class Attack1 : Leaf
    {
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public override NodeResult Execute()
        {
            _animator.Play("Attack1");
            return NodeResult.success;
        }
    }
}
using System;
using MBT;
using UnityEngine;

namespace BT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/Attack2")]
    public class Attack2 : Leaf
    {
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public override NodeResult Execute()
        {
            _animator.Play("Attack2");
            return NodeResult.success;
        }
    }
}
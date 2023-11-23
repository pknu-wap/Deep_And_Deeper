using System;
using MBT;
using UnityEngine;

namespace Monster.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Monster/MeleeAttack")]
    public class MeleeAttack : Leaf
    {
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public override NodeResult Execute()
        {
            _animator.Play("MeleeAttack");
            return NodeResult.success;
        }
    }
}
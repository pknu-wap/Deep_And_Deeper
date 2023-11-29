using System;
using MBT;
using UnityEngine;

namespace BT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/Jump")]
    public class Jump : Leaf
    {
        private Animator _animator;
        //private Rigidbody2D _rigidbody2D;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            //_rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public override NodeResult Execute()
        {
            _animator.Play("Jump");
            return NodeResult.success;
        }
    }
}
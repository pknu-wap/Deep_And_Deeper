using System;
using MBT;
using Unity.VisualScripting;
using UnityEngine;

namespace BT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/CheckGrounded")]
    public class CheckGrounded : Leaf
    {
        private bool _isGrounded;

        private void OnCollisionEnter2D(Collision2D other)
        {
           _isGrounded = true;
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            _isGrounded = false;
        }

        public override NodeResult Execute()
        {
            if (_isGrounded == true) return NodeResult.success;
            
            return NodeResult.failure;
        }
    }
}
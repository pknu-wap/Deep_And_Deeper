using System;
using MBT;
using UnityEngine;

namespace BT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/Reset")]
    public class Reset: Leaf
    {
        private BoxCollider2D _boxCollider2D;

        private void Start()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }

        public override NodeResult Execute()
        {
            _boxCollider2D.size = new Vector2(_boxCollider2D.size.x, 1.3f);
            _boxCollider2D.offset = new Vector2(_boxCollider2D.offset.x, 0.7f);
            
            return NodeResult.success;
        }
    }
}
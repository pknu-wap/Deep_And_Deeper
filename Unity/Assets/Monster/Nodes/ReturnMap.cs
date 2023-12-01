using MBT;
using UnityEngine;

namespace Monster.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Monster/ReturnMap")]
    public class ReturnMap : Leaf
    {
        private SpriteRenderer _sprite;
        private CapsuleCollider2D _collider;
        private bool _created;

        private void Start()
        {
            var gameObj = GameObject.FindGameObjectWithTag("RetPortal");
            _sprite = gameObj.GetComponent<SpriteRenderer>();
            _collider = gameObj.GetComponent<CapsuleCollider2D>();
        }

        public override NodeResult Execute()
        {
            if (_created) return NodeResult.success;

            _created = true;
            _sprite.enabled = true;
            _collider.enabled = true;

            return NodeResult.success;
        }
    }
}
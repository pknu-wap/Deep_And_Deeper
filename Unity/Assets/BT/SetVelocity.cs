using MBT;
using UnityEngine;

namespace BT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/SetVelocity")]
    public class SetVelocity : Leaf
    {
        [SerializeField] private bool x, y;
        [SerializeField] private float velocity;
        private Rigidbody2D _rigidbody2D;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public override NodeResult Execute()
        {
            _rigidbody2D.velocity = new Vector2(x ? velocity : _rigidbody2D.velocity.x,
                y ? velocity : _rigidbody2D.velocity.y);
            return NodeResult.success;
        }
    }
}
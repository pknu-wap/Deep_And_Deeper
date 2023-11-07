using MBT;
using UnityEngine;

namespace Hero.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/SetVelocity")]
    public class SetVelocity : Leaf
    {
        [SerializeField] private bool x, y;
        [SerializeField] private float velocity;

        private void Awake()
        {
            Debug.Assert(x ^ y);
        }

        public override NodeResult Execute()
        {
            if (x) HeroManager.Instance.SetVelocityX(velocity);
            if (y) HeroManager.Instance.SetVelocityY(velocity);

            return NodeResult.success;
        }
    }
}
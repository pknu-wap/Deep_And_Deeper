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

        public override NodeResult Execute()
        {
            HeroManager.Instance.SetVelocity(x ? velocity : null, y ? velocity : null);
            return NodeResult.success;
        }
    }
}
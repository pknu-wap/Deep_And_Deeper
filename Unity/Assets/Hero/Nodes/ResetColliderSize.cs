using MBT;
using UnityEngine;

namespace Hero.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/ResetColliderSize")]
    public class ResetColliderSize : Leaf
    {
        public override NodeResult Execute()
        {
            HeroManager.Instance.ResetColliderSize();

            return NodeResult.success;
        }
    }
}
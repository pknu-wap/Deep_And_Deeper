using MBT;
using UnityEngine;

namespace Hero.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/CheckGrounded")]
    public class CheckGrounded : Leaf
    {
        public override NodeResult Execute()
        {
            return HeroManager.Instance.isGrounded ? NodeResult.success : NodeResult.failure;
        }
    }
}
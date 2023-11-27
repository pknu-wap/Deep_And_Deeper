using MBT;
using UnityEngine;

namespace Hero.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/CheckDead")]
    public class CheckDead : Leaf
    {
        public override NodeResult Execute()
        {
            return HeroManager.Instance.IsDead ? NodeResult.success : NodeResult.failure;
        }
    }
}
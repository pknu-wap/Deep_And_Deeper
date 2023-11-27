using MBT;
using UnityEngine;

namespace Hero.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/IsDead")]
    public class IsDead : Leaf
    {
        public override NodeResult Execute()
        {
            return NodeResult.success;
        }
    }
}
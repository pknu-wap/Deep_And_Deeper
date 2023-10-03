using MBT;
using UnityEngine;

namespace Monster.Nodes
{
    [AddComponentMenu("")]
    [MBTNode(name = "Monster/Patrol")]
    public class Patrol : Leaf
    {
        public override NodeResult Execute()
        {
            return NodeResult.success;
        }
    }
}
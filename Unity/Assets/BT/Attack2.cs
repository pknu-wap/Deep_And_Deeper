using MBT;
using UnityEngine;

namespace BT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/Attack2")]
    public class Attack2 : Leaf
    {
        public override NodeResult Execute()
        {
            Debug.Log("Attack2");
            return NodeResult.success;
        }
    }
}
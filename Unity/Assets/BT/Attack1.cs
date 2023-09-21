using MBT;
using UnityEngine;

namespace BT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/Attack1")]
    public class Attack1 : Leaf
    {
        public override NodeResult Execute()
        {
            Debug.Log("Attack1");
            return NodeResult.success;
        }
    }
}
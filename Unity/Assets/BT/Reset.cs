using MBT;
using UnityEngine;

namespace BT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/Reset")]
    public class Reset : Leaf
    {
        public override NodeResult Execute()
        {
            return NodeResult.success;
        }
    }
}
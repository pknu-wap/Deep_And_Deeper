using MBT;
using UnityEngine;

namespace BT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Hero/MoveX")]
    public class MoveX : Leaf
    {
        public override NodeResult Execute()
        {
            return NodeResult.success;
        }
    }
}
using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Decorators/Repeater")]
    public class Repeater : Decorator
    {
        public int loops = 1;
        public bool infinite;
        private int count;
        
        public override void OnEnter()
        {
            count = loops;
        }

        public override NodeResult Execute()
        {
            var node = GetChild();
            if(node == null) {
                return NodeResult.failure;
            }

            if (!infinite && count <= 0) return NodeResult.success;
            // Repeat children
            behaviourTree.ResetNodesTo(this);
            count -= 1;
            return node.runningNodeResult;
        }
    }
}

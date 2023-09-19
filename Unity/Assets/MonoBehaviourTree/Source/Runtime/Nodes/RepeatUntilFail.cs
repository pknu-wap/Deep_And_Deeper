using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode("Decorators/Repeat Until Fail")]
    public class RepeatUntilFail : Decorator
    {
        public override NodeResult Execute()
        {
            var node = GetChild();
            if (node.status == Status.Failure) {
                return NodeResult.success;
            }
            // Repeat children
            behaviourTree.ResetNodesTo(this);
            return node.runningNodeResult; 
        }
    }
}

using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode("Decorators/Succeeder")]
    public class Succeeder : Decorator
    {
        public override NodeResult Execute()
        {
            var node = GetChild();
            if (node != null && node.status is Status.Success or Status.Failure) {
                return NodeResult.success;
            }
            return node.runningNodeResult;
        }
    }
}

using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode("Decorators/Inverter")]
    public class Inverter : Decorator
    {
        public override NodeResult Execute()
        {
            var node = GetChild();
            if (node == null) {
                return NodeResult.failure;
            }

            return node.status switch
            {
                Status.Success => NodeResult.failure,
                Status.Failure => NodeResult.success,
                _ => node.runningNodeResult
            };
        }
    }
}

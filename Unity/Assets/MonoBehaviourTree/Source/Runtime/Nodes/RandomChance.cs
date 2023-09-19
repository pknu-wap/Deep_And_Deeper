using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode("Decorators/Random Chance")]
    public class RandomChance : Decorator
    {
        [Tooltip("Probability should be between 0 and 1")]
        public FloatReference probability = new FloatReference(0.5f);
        private float roll;

        public override void OnAllowInterrupt()
        {
            roll = Random.Range(0f, 1f);
        }

        public override NodeResult Execute()
        {
            var node = GetChild();
            if (node == null) {
                return NodeResult.failure;
            }
            if (node.status is Status.Success or Status.Failure) {
                return NodeResult.From(node.status);
            }
            return roll > probability.Value ? NodeResult.failure : node.runningNodeResult;
        }

        private void OnValidate()
        {
            if (probability.isConstant)
            {
                probability.Value = Mathf.Clamp(probability.GetConstant(), 0f, 1f);
            }
        }
    }
}

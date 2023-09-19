using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Parallel", order = 160)]
    public class Parallel : Composite
    {
        private int index;
        
        public override void OnAllowInterrupt()
        {
            if (random)
            {
                ShuffleList(children);
            }
        }
        
        public override void OnEnter()
        {
            index = 0;
        }

        public override void OnBehaviourTreeAbort()
        {
            // Do not continue from last index
            index = 0;
        }

        public override NodeResult Execute()
        {
            var res = NodeResult.success;
            while (index < children.Count)
            {
                var child = children[index];
                switch (child.status)
                {
                    case Status.Success:
                        index += 1;
                        continue;
                    case Status.Failure:
                        index += 1;
                        res = NodeResult.failure;
                        continue;
                }
                return child.runningNodeResult;
            }
            return res;
        }
    }
}

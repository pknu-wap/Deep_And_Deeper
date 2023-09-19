using UnityEngine;

namespace MBT
{
    public abstract class Service : Decorator
    {
        public float interval = 1f;
        public float randomDeviation;
        public bool callOnEnter = true;
        /// <summary>
        /// Time of the next update of the task
        /// </summary>
        private float nextScheduledTime;

        public override void OnEnter()
        {
            // Set time of the next update
            nextScheduledTime = Time.time + interval + Random.Range(-randomDeviation, randomDeviation);
            behaviourTree.onTick += OnBehaviourTreeTick;
            if (callOnEnter)
            {
                Task();
            }
        }

        public override NodeResult Execute()
        {
            var node = GetChild();
            if (node == null) {
                return NodeResult.failure;
            }
            return node.status is Status.Success or Status.Failure ? NodeResult.From(node.status) : node.runningNodeResult;
        }

        protected abstract void Task();

        public override void OnExit()
        {
            behaviourTree.onTick -= OnBehaviourTreeTick;
        }

        private void OnBehaviourTreeTick()
        {
            if (!(nextScheduledTime <= Time.time)) return;
            // Set time of the next update and run the task
            nextScheduledTime = Time.time + interval + Random.Range(-randomDeviation, randomDeviation);
            Task();
        }

        protected virtual void OnValidate()
        {
            interval = Mathf.Max(0f, interval);
            randomDeviation = Mathf.Clamp(randomDeviation, 0f, interval);
        }
    }
}
